#!/bin/bash
# Script para Realizar update automaticamente conforme nova versão
# Checa se existe, baixa, roda o build e copia para o diretorio da aplicação
# Fiz apenas para simplificar o processo.
REMOTE_VERSION_URL="https://raw.githubusercontent.com/tcruzf/AuthProjeto/refs/heads/main/version.info"
PROJECT_DIR="/var/www/public_html"
PROJECT_FOLDER="app"
LOG_PATH="/tmp/AuthProject.log"
DATE=$(date +%d-%m-%Y)
TMP_DIR="/tmp/build"
echo "Remota: $(curl -s $REMOTE_VERSION_URL)"

_temp() {
    rm -rf "$TMP_DIR" && mkdir -p "$TMP_DIR"
    cd "$TMP_DIR" || exit 1
    touch "$LOG_PATH"
}

_makeLogsDir() {
    mkdir -p "$PROJECT_DIR/$PROJECT_FOLDER/logs"
}

_downloadNew() {
    echo "Criando cópia do arquivo de logs"
    cp "$LOG_PATH" "/tmp/Slog.tmp"

    echo "Baixando nova versão"
    git clone -q https://github.com/tcruzf/AuthProjeto.git "$TMP_DIR/AuthProjeto"

    echo "Restaurando e construindo projeto..."
    cd "$TMP_DIR/AuthProjeto" && /usr/bin/dotnet restore
    /usr/bin/dotnet build
    /usr/bin/dotnet publish -c Release -o ./publish --self-contained false

    echo "Realizando backup da versão atual"
    rm -rf "$PROJECT_DIR/${DATE}${PROJECT_FOLDER}"
    mv "$PROJECT_DIR/$PROJECT_FOLDER" "$PROJECT_DIR/${DATE}${PROJECT_FOLDER}"

    echo "Instalando nova versão"
    mv "$TMP_DIR/AuthProjeto/publish" "$PROJECT_DIR/app"
    #cp "/tmp/Slog.tmp" "$PROJECT_DIR/$PROJECT_FOLDER/logs/"
    cp "$TMP_DIR/version.info" "$PROJECT_DIR/$PROJECT_FOLDER/"
    echo "Start Services..."
    _startServices
}

_checkVersion() {
    cd "$TMP_DIR" || return 1
    rm -f version.info

    if ! wget -q "$REMOTE_VERSION_URL"; then
        echo "$DATE | ERRO: Falha ao baixar versão remota" >> "$LOG_PATH"
        return 1
    fi

    VERSION=$(< "$PROJECT_DIR/$PROJECT_FOLDER/version.info")
    NEW_VERSION=$(< "$TMP_DIR/version.info")

    echo "Versão Local: $VERSION" >> "$LOG_PATH"
    echo "Versão Remota: $NEW_VERSION" >> "$LOG_PATH"

    if [ "$NEW_VERSION" -gt "$VERSION" ]; then
        echo "$DATE | NOVA VERSÃO DETECTADA: $NEW_VERSION" >> "$LOG_PATH"
        return 0
    fi

    echo "$DATE | Versão atual já instalada: $VERSION" >> "$LOG_PATH"
    return 1
}

_startServices() {
    echo "$DATE | Reiniciando serviços" >> "$LOG_PATH"
    systemctl restart controllrr.service
    service nginx restart
}

_start() {
    _temp
    _makeLogsDir

    if _checkVersion; then
        echo "Iniciando atualização..."
        _downloadNew
    else
        echo "Versao remota: $NEW_VERSION"
        echo "Versao local: $VERSION"
        echo "Nenhuma atualização necessária"
    fi

    echo "Status final: $DATE | $(date +%H:%M:%S)" >> "$LOG_PATH"
}

_start
