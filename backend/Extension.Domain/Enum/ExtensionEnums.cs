using System.ComponentModel;

namespace Extension.Domain.Enum
{
    public static class ExtensionEnums
    {
        public enum MessageCode
        {
            [Description("Is Valid")]
            IsValid = 100,

            [Description("Successfully")]
            Success = 200,

            [Description("No Content")]
            NoContent = 204,

            [Description("Not Valid")]
            NotValid = 400,

            [Description("Not Found")]
            NotFound = 404,

            [Description("Unexpected")]
            Exeption = 500,
        }
    }
}
