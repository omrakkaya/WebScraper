namespace ScrappyBot.Models.Dto
{
    public class ResponseDto
    {
        public object? Item { get; set; }
        public string Message { get; set; } = " ";
        public bool IsSuccess { get; set; } = true;
    }
}
