namespace SanitexVoiceMAUI.Models
{
    public class VoiceApiResponse
    {
        public List<VoiceCommands> Commands { get; set; }
        public string Labels { get; set; }
        public string StateName { get; set; }
        public string Workflow { get; set; }
        public int DataType { get; set; }
        public int Scanner { get; set; }
        public string TriggersTrace { get; set; }
    }

    public class VoiceCommands
    {
        public string Name { get; set; }
        public List<string> Variants { get; set; }
        public bool RequestCheck { get; set; }
        public bool IsDefault { get; set; }
    }
}
