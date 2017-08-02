//
//  UJSInterface.m
//  Unity-iPhone
//
//  Created by MacMini on 14-5-15.
//
//

#import "WXPayInterface.h"
#import "WXApiManager.h"

@implementation WXPayInterface

void _WXBuyProduct(void *partnerId,void *waresid,void *orderid,UInt32 timeStamp,void *sign){
    [[WXApiManager sharedManager] buyRequest:[NSString stringWithUTF8String:partnerId]
		prepayId:[NSString stringWithUTF8String:waresid]
		nonceStr:[NSString stringWithUTF8String:orderid]
		timeStamp:timeStamp
		sign:[NSString stringWithUTF8String:sign]];
}

@end
