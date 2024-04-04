namespace EcommerceBackEnd.Services
{
    public interface IFileStorage
    {
        // this interface pretends to interact with the azure storage created
        // container represents a folder on azure storage
        // contenido: This is the byte array representing the content of the file.
        // extension: This is the file extension (e.g., ".jpg", ".pdf", etc.).
        // contentType: This is the MIME type of the file content.
        Task<string> SaveFile(byte[] content, string extension, string container, string contentType);
        Task<string> EditFile(byte[] content, string extension, string container, string route,  string contentType);
        Task<string> DeleteFile(string route, string container);

    }
}
