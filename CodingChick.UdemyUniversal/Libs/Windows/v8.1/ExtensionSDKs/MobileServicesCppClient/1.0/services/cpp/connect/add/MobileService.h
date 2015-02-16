#pragma once

#include <azuremobile.h>

namespace AzureMobileHelper
{
    /// <summary>
    /// $MakeSafeIdentifierName(ServiceInstance.Name)$MobileService contains support for contacting an associated mobile service.
    /// </summary>
    class $MakeSafeIdentifierName(ServiceInstance.Name)$MobileService
    {
    public:
        static azure::mobile::client& GetClient();
    };
}
