namespace ControllRR.Domain.Entities;
public class Document
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string DocumentName { get; set; }
    public DateTime UploadedAt { get; set; }


    public Document()
    {

    }
    public Document(int id, string description, string documentName, DateTime uploadedAt)
    {
        Id = id;
        Description = description;
        DocumentName = documentName;
        UploadedAt = DateTime.Now;
    }


}