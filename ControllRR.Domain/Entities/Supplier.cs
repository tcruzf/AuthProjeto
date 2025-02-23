namespace ControllRR.Domain.Entities;


public class Supplier
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? CNPJ { get; set; }
    public string? ContactEmail { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }

    public Supplier()
    {

    }

    public Supplier(int id, string? name, string cnpj, string contactEmail, string phoneNumber, string address)
    {
        if(!ValidarCNPJ(cnpj))
            throw new ArgumentException("CNPJ Invalido");
        Id = id;
        Name = name;
        CNPJ = cnpj;
        ContactEmail = contactEmail;
        PhoneNumber = phoneNumber;
        Address = address;
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