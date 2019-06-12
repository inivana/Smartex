class ServerAnswerRecievedPost : ServerFeedback
{
    [JsonProperty(PropertyName = "result")]
    public Post Post { get; set; }
}
