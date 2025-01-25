using System.ComponentModel.DataAnnotations;

namespace ControllRR.Domain.Entities;
public class Sector
{
    public int Id { get; set; }

    [Display(Name = "Setor")]
    [Required(ErrorMessage ="O campo {0} é obrigatorio")]
    public string Name { get; set; }
   
    [Display(Name = "Local")]
    [Required(ErrorMessage ="O campo {0} é obrigatorio")]
    public string Location { get; set; }
  
    [Display(Name = "Solicitante")]
    [Required(ErrorMessage ="O campo {0} é obrigatorio")]
    public string RequesterName { get; set; }
    [Display(Name = "Endereço")]
    [Required(ErrorMessage ="O campo {0} é obrigatorio")]
    public string Address { get; set; }
    [Display(Name = "Numero")]
    [Required(ErrorMessage ="O campo {0} é obrigatorio")]
    public string Number { get; set; }
     [Display(Name = "Bairro")]
     [Required(ErrorMessage ="O campo {0} é obrigatorio")]
    public string Neighborhood { get; set; }
     [Display(Name = "Cidade")]
     [Required(ErrorMessage ="O campo {0} é obrigatorio")]
    public string City { get; set; }
     [Display(Name = "Cep")]
     [Required(ErrorMessage ="O campo {0} é obrigatorio")]
    public string Cep { get; set; }

    public ICollection<Device>? Devices {get; set; }

    public Sector()
    {

    }
    public Sector(int id, string name, string location, string address, string number, string neighborhood, string city, string cep, string requesterName)
    {
        Id = id;
        Name = name;
        Location = location;
        Address = address;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        Cep = cep;
        RequesterName = requesterName;
        
    }

}