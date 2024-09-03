using System.Security.Cryptography;
using System.Text;
using PasswordFeedbackManager.Models;


namespace PasswordFeedbackManager.Services;

public class PasswordFeedbackService : IPasswordFeedbackService
{
    private readonly List<PasswordFeedbackModel> _passwordFeedbacks = new ();

    public string AddCombination(PasswordFeedbackModel passwordFeedback)
    {
        try
        {
            if (passwordFeedback == null)
                return "false";
            else if (_passwordFeedbacks.Any(pf => pf.Password.Equals(passwordFeedback.Password)))
                return "the feedback for this password is already taken";
            else
            {
                _passwordFeedbacks.Add(passwordFeedback);
                return "true";
            }
        }
        catch (Exception ex)
        {
            return $"An error occurred: {ex.Message}";
        }

    }

    public string GetFeedback(string password)
    {
        try
        {
            if (string.IsNullOrEmpty(password)) return null;
            var item = _passwordFeedbacks.FirstOrDefault(pf => pf.Password.Equals(password));

            return item != null ? string.Join(" ", item.Feedback.ToCharArray()) : null;
        }
        catch (Exception ex)
        {
            return $"An error occurred: {ex.Message}";
        }
    }

    #region PasswordHash

    private static string GetSHA256Hash(string input)
    {
        try
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        catch (Exception ex)
        {
            return $"An error occurred: {ex.Message}";
        }
    }

    public string GetPasswordHash(string feedback)
    {
        try
        {
            var item = _passwordFeedbacks.FirstOrDefault(pf => pf.Feedback.Equals(feedback));
            if (item == null) return null;

            return GetSHA256Hash(item.Password);
        }
        catch (Exception ex)
        {
            return $"An error occurred: {ex.Message}";
        }
    }

    #endregion

    public int GetCombinationsCount()
    {
        try
        {
            return _passwordFeedbacks.Count;
        }
        catch
        {
            return -1;
        }
    }
}