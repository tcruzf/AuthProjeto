using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ControllRR.Application.Dto;

public class DocumentDto
{
    public int Id { get; set; }
    [Display(Name = "Descrição")]
    [Required(ErrorMessage = "A descrição completa é obrigatória.")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "{0} é obrigatório.")]
    [StringLength(5, MinimumLength = 15, ErrorMessage = "{0} deve ter entre {2} e  {1} caracteres!")]
    public string? DocumentName { get; set; }
    [Display(Name = "Data de Upload")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public IFormFile FormFile { get; set; }

    public DateTime UploadedAt { get; set; }

}