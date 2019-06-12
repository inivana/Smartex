class ServerAnswerRecievedEvent : ServerFeedback
{
    [JsonProperty(PropertyName = "result")]
    public Event Event_ { get; set; }
}
