using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GeoGame.Services;

public class QuizController : Controller
{
    private readonly IGeographyService _geographyService;

    public QuizController(IGeographyService geographyService)
    {
        _geographyService = geographyService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult StartQuiz(string quizType)
    {
        var quizModel = new QuizModel();
        switch (quizType)
        {
            case "European capitals":
                quizModel.Question = _geographyService.GetRandomEuropeanCountry();
                quizModel.CorrectAnswer = _geographyService.GetEuropeanCapital(quizModel.Question);
                return View("EuropeanCapitals", quizModel);
            case "Asian capitals":
                quizModel.Question = _geographyService.GetRandomAsianCountry();
                quizModel.CorrectAnswer = _geographyService.GetAsianCapital(quizModel.Question);
                return View("AsianCapitals", quizModel);
            case "African capitals":
                quizModel.Question = _geographyService.GetRandomAfricanCountry();
                quizModel.CorrectAnswer = _geographyService.GetAfricanCapital(quizModel.Question);
                return View("AfricanCapitals", quizModel);
            case "World capitals":
                quizModel.Question = _geographyService.GetRandomCountry();
                quizModel.CorrectAnswer = _geographyService.GetCapital(quizModel.Question);
                return View("WorldCapitals", quizModel);
            case "World countries":
                quizModel.Question = _geographyService.GetRandomCountry();
                quizModel.Continent = _geographyService.GetContinent(quizModel.Question);
                quizModel.CountryCode = _geographyService.GetAlpha2Code(quizModel.Question);
                quizModel.CorrectAnswer = quizModel.Question;
                return View("WorldCountries", quizModel);
            case "World flags":
                quizModel.Question = _geographyService.GetRandomCountry();
                quizModel.CountryCode = _geographyService.GetAlpha2Code(quizModel.Question);
                return View("WorldFlags", quizModel);
            default:
                return View("Index");
        }
    }

    [HttpPost]
    public IActionResult CheckAnswer(QuizModel quizModel, string quizType)
    {
        quizModel.AnsweredCorrectly = quizModel.UserAnswer == quizModel.CorrectAnswer;
        int correctAnswers = quizModel.AnsweredCorrectly ? quizModel.PreviousCorrectAnswers + 1 : quizModel.PreviousCorrectAnswers;
        string redirectView = string.Empty;

        quizModel.PreviousCorrectAnswers = correctAnswers;
        
        return View(redirectView, quizModel);
    }
}
