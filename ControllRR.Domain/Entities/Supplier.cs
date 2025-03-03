using ControllRR.Domain.Enums;

namespace ControllRR.Domain.Entities;
 

public class Supplier
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? FantasyName { get; set; }
    public string? CNPJ { get; set; }
    public string? ContactEmail { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? CEP { get; set; }
    public string? CRT { get; set; }
    public string? IssuerIE { get; set; }
    public PersonType? PersonType { get; set; }
    public string? ActivityArea { get; set; }



    public Supplier()
    {

    }

    public Supplier(int id,
     string? name,
     string fantasyName,
     string cnpj,
     string contactEmail,
     string phoneNumber,
     string address,
     string? cep,
     string? crt,
     string? issuerIE,
     PersonType? personType,
     string? activityArea  
     )
    {
        if(string.IsNullOrWhiteSpace(cnpj))
            throw new ArgumentException("CNPJ Não pode ser vazio ou conter espaços!");
        // Estou tendo problemas com a validação do cnpj, não sei ao certo, mas pode ser o cnpj-mask no fronte-end.
        // Quando insiro um cnpj, ele define o padrão como : 00.000.000/0000-00, isso provavelmente está estourando o maximo permitido no DTO.
        // Sendo assim, aumentei o campo de cnpj para aceitar mais caracteres, embora saiba que isso é um erro e amanhã vou fazer a correção disso.
        if(!ValidarCNPJ(cnpj))
            throw new ArgumentException("CNPJ não pode ser validado! Verifique o numero de CNPJ e tente novamente. ");
        Id = id;
        Name = name;
        FantasyName = fantasyName;
        CNPJ = cnpj;
        ContactEmail = contactEmail;
        PhoneNumber = phoneNumber;
        Address = address;
        CEP = cep;
        CRT = crt;
        IssuerIE = issuerIE;
        PersonType = personType;
        ActivityArea = activityArea;


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