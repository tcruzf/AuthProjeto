using System.ComponentModel.DataAnnotations;
using ControllRR.Application.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControllRR.Presentation.ViewModels;
public class MessagesViewModel
{
   public TempMessageDto? tempMessageDto  { get; set; }
}