namespace GlencoreWordGame.Models.Response
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public ErrorResponse Error { get; set; }
    }
}
