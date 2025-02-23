namespace ControllRR.Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? CPF_CNPJ { get; set; }
    public string? ContactInfo { get; set; }

    public Customer()
    {

    }

    public Customer(int id, string? name, string? cpfCnpj, string? contactInfo)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome obrigatório");
        
        if(string.IsNullOrWhiteSpace(cpfCnpj))
            throw new ArgumentException("Documento obrigatório");

        cpfCnpj = new string(cpfCnpj.Where(char.IsDigit).ToArray());
        
        if(cpfCnpj.Length == 11)
        {
            if(!ValidarCPF(cpfCnpj))
                throw new ArgumentException("CPF Inválido!");
        }
        else if(cpfCnpj.Length == 14)
        {
            if(!ValidarCNPJ(cpfCnpj))
                throw new ArgumentException("CNPJ Inválido!");

        }
        else
        {
            throw new ArgumentException("Documento Inválido");
        }

        Id = id;
        Name = name;
        CPF_CNPJ = cpfCnpj ?? throw new ArgumentNullException(nameof(cpfCnpj));
        ContactInfo = contactInfo;

    }

     public static bool ValidarCPF(string cpf)
    {
        cpf = new string(cpf.Where(char.IsDigit).ToArray());
        if (cpf.Length != 11 || cpf.Distinct().Count() == 1) return false;
        
        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        
        string tempCpf = cpf.Substring(0, 9);
        int soma = tempCpf.Select((t, i) => (t - '0') * multiplicador1[i]).Sum();
        int resto = (soma * 10) % 11;
        if (resto == 10) resto = 0;
        if (resto != (cpf[9] - '0')) return false;
        
        tempCpf += resto;
        soma = tempCpf.Select((t, i) => (t - '0') * multiplicador2[i]).Sum();
        resto = (soma * 10) % 11;
        if (resto == 10) resto = 0;
        return resto == (cpf[10] - '0');
    }

    public static bool ValidarCNPJ(string cnpj)
    {
        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());
        if (cnpj.Length != 14 || cnpj.Distinct().Count() == 1) return false;
        
        int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        
        string tempCnpj = cnpj.Substring(0, 12);
        int soma = tempCnpj.Select((t, i) => (t - '0') * multiplicador1[i]).Sum();
        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        if (resto != (cnpj[12] - '0')) return false;
        
        tempCnpj += resto;
        soma = tempCnpj.Select((t, i) => (t - '0') * multiplicador2[i]).Sum();
        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        return resto == (cnpj[13] - '0');
    }



}