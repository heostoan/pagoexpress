namespace UW.AdminPagoExpress.Shared.Common;

public class ApiResult
{
    public bool EsExitoso { get; init; }
    public string? Error { get; init; }

    public static ApiResult Success() => new() { EsExitoso = true };
    public static ApiResult Failure(string error) => new() { EsExitoso = false, Error = error };
}

public class ApiResult<T> : ApiResult
{
    public T? Valor { get; init; }

    public static ApiResult<T> Success(T valor) =>
        new() { EsExitoso = true, Valor = valor };

    public static new ApiResult<T> Failure(string error) =>
        new() { EsExitoso = false, Error = error };
}