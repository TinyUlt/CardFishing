/* //
//  UJSInterface.m
//  Unity-iPhone
//
//  Created by MacMini on 14-5-15.
//
//

#import "IapPayInterface.h"
#import "IapPayManager.h"

@implementation IapPayInterface

IapPayManager *iapManager = nil;

void _InitIAPManager(){
    iapManager = [[IapPayManager alloc] init];
    [iapManager initPayKit];
}

void _IAPBuyProduct0(void *userid,void *waresid,void *orderid){
    [iapManager buyRequest:[NSString stringWithUTF8String:userid] 
		waresid:[NSString stringWithUTF8String:waresid]
		orderid:[NSString stringWithUTF8String:orderid]];
}

void _IAPBuyProduct1(void *trandInfo){
    [iapManager buyRequest:[NSString stringWithUTF8String:trandInfo]];
}

void _IAPDestroy()
{
	iapManager = nil;
}

@end
 */