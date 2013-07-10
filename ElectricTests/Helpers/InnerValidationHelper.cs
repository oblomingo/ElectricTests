using System.Data.Entity.Validation;
using System.Text;

namespace ElectricTests.Helpers
{
    public static class InnerValidationHelper
    {
        /// <summary>
        /// Get entity validation errors from exception object
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetEntityInnerValidationErrors(DbEntityValidationException e)
        {
            var sb = new StringBuilder();
            foreach (var failure in e.EntityValidationErrors)
            {
                sb.AppendFormat("{0} klaida", failure.Entry.Entity.GetType());
                foreach (var error in failure.ValidationErrors)
                {
                    sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }
    }
}