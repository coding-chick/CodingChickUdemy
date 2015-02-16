#pragma once

#include <azuremobile.h>
#include "$ServiceInstanceRootPath$\$MakeSafeIdentifierName(ServiceInstance.Name)$MobileService.h"

namespace AzureMobileHelper
{
    class $MakeSafeIdentifierName(ServiceInstance.Name)$Push
    {
    public:
        static void UploadChannel();
    private:
        static void HandleExceptionsComingFromTheServer();
    };
}
