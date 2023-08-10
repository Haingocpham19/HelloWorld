namespace Extension.Domain.Enum
{
    public static class ExtensionEnums
    {
        public enum ExtensionCode
        {
            /// <summary>
            /// Dữ liệu hợp lệ
            /// </summary>
            IsValid = 100,

            /// <summary>
            /// Thành công
            /// </summary>
            Success = 200,

            /// <summary>
            /// không có nội dung
            /// </summary>
            NoContent = 204,

            /// <summary>
            /// Dữ liệu không hợp lệ
            /// </summary>
            NotValid = 400,

            /// <summary>
            /// không tìm thấy dữ liệu
            /// </summary>
            NotFound = 404,

            /// <summary>
            /// Lỗi
            /// </summary>
            Exeption = 500,
        }
    }
}
