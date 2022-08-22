namespace ProjekatRVA.Models.Dto.EventDto
{
    public class AddEventDto
    {
        public string Text { get; set; }
        public string DateAndTime { get; set; }
        public int PlannerId { get; set; }
        public string Token { get; set; }
    }
}
