using PasswordFeedbackManager.Models;


namespace PasswordFeedbackManager.Services
{
    public interface IPasswordFeedbackService
    {
        /// <summary>
        /// Adding a new password-revocation combination
        /// </summary>
        /// <param name="passwordFeedback"></param>
        /// <returns></returns>
        string AddCombination(PasswordFeedbackModel passwordFeedback);

        /// <summary>
        /// Getting feedback by password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        string GetFeedback(string password);

        /// <summary>
        /// Getting password by feedback
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns></returns>
        string GetPasswordHash(string feedback);

        /// <summary>
        /// Getting the number of combinations
        /// </summary>
        /// <returns></returns>
        int GetCombinationsCount();
    }
}