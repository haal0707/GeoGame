using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class QuizModel
{
    public string? Question { get; set; }
    public string? CorrectAnswer { get; set; }
    public string? UserAnswer { get; set; }
    public bool AnsweredCorrectly { get; set; }
    public int PreviousCorrectAnswers { get; set; }
    public string? CountryCode { get; set; }
    public string? Continent { get; set; }

}