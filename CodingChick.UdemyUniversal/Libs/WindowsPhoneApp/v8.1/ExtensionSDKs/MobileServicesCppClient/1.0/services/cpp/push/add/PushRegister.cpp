#include "pch.h"
#include "$ServiceInstanceRootPath$\$MakeSafeIdentifierName(ServiceInstance.Name)$Push.h"

// http://go.microsoft.com/fwlink/?LinkID=290991&clcid=$vslcid$

using namespace AzureMobileHelper;

using namespace web;
using namespace concurrency;

using namespace Windows::Networking::PushNotifications;

void $MakeSafeIdentifierName(ServiceInstance.Name)$Push::UploadChannel()
{
    create_task(PushNotificationChannelManager::CreatePushNotificationChannelForApplicationAsync()).
    then([] (PushNotificationChannel^ newChannel) 
    {
        return $MakeSafeIdentifierName(ServiceInstance.Name)$MobileService::GetClient().get_push().register_native(newChannel->Uri->Data());
    }).then([]()
    {
        return $MakeSafeIdentifierName(ServiceInstance.Name)$MobileService::GetClient().invoke_api(L"notifyAllUsers");
    }).then([](task<json::value> result)
    {
        // http://go.microsoft.com/fwlink/?LinkID=290991&clcid=$vslcid$		
        try
        {
            result.wait();
        }
        catch(...)
        {
            HandleExceptionsComingFromTheServer();
        }
    });
}

void $MakeSafeIdentifierName(ServiceInstance.Name)$Push::HandleExceptionsComingFromTheServer()
{
}
