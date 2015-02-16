#include "pch.h"
#include "$ServiceInstanceRootPath$\$ServiceInstance.Name$MobileService.h"

using namespace AzureMobileHelper;

/// <summary>
/// Returns the client instance representing the associated mobile service account.
/// </summary>
/// <returns>A non-const reference to the client instance.</returns>
azure::mobile::client& $MakeSafeIdentifierName(ServiceInstance.Name)$MobileService::GetClient()
{
    static azure::mobile::client c(L"$ServiceInstance.ApplicationUrl$", 
                                   L"$ServiceInstance.ApplicationKey$");
    return c;
}
