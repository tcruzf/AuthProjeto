using System.ComponentModel.DataAnnotations;

namespace ControllRR.Domain.Entities;

    public class User 
    {
        public int Id { get; set; }
        [Display(Name="Nome")]
         [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} minimo {2} e no maximo {1} caracteres")]
        public string Name { get; set; }
        [Display(Name = "Telefone")]
         [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} minimo {2} e no maximo {1} caracteres")]
        public string Phone { get; set; }
        [Display(Name = "Matricula")]
        public double Register { get; set; }
        public ICollection<Maintenance>? Maintenances {get; set; }

        public User()
        {

        }
        public User(int id, string name, string phone, double register)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Register = register;
          // Department = department;

        }

    }
