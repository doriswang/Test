namespace Test.Framework.Validation
{
    using System;
    using System.Text;

    public class EntityValidator : IValidator
    {
        private Validation[] validations;
        public EntityValidator(params Validation[] validations)
        {
            this.message = string.Empty;
            this.validations = validations;
        }

        private bool isValid { get; set; }
        public bool IsValid { get { return isValid; } }

        private string message { get; set; }
        public string Message { get { return message; } }

        public void Validate()
        {
            var errors = new StringBuilder();

            foreach (Validation validation in validations)
            {
                bool invalid = validation.Expression.Compile().Invoke();

                if (invalid)
                {
                    errors.AppendLine(string.Format("{0}", validation.ErrorMessage));
                }
            }

            if (errors.Length > 0)
            {
                this.isValid = false;
                this.message = errors.ToString();
            }
            else
            {
                this.isValid = true;
            }
        }
    }
}
