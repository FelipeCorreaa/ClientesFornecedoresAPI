namespace APIClienteFornecedor.Services.Tokren
{
    public interface ITokenService
    {
        string GenerateToken(string accessKeyId, string accessKeySecret);
    }
}
