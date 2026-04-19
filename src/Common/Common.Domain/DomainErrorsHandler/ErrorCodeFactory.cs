namespace Common.Domain.DomainErrorsHandler;

using Common.Domain.DomainErrorsHandler.Enums;

public static class ErrorCodeFactory
{
    public static string Create(string token, EnDomainType domainType, EnErrorType errorType,
        EnUniqueKeyErrorType uniqueKeyErrorType, string fild) =>
        $"{token}_{domainType}_{errorType}_{(int)uniqueKeyErrorType}_{fild}";
}
