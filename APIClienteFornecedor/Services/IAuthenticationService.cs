namespace APIClienteFornecedor.Services
{
    public interface IAuthenticationService
    {
        string GenerateToken(string accessKeyId, string accessKeySecret);
        public string ReadTokenFromFile();

    }
}