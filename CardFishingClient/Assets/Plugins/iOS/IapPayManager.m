/* //
//  IAPManager.m
//  Unity-iPhone
//
//  Created by MacMini on 14-5-16.
//
//
#import "IapPayManager.h"
#import "OrderUtils.h"
#import <IapppayKit/IapppayKit.h>
@interface IapPayManager ()
<IapppayKitPayRetDelegate>
@end



@implementation IapPayManager

//渠道号
static NSString *mOrderUtilsChannel = @"999";

//商户在爱贝注册的应用ID
static NSString *mOrderUtilsAppId = @"wx74d43714b21f57c5";

//支付结果后台回调地址
static NSString *mOrderUtilsNotifyurl = @"http://paycallback.djl28.com:8098/game/iappay.do";

//商户验签公钥
static NSString *mOrderUtilsCheckResultKey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCv2ap2cqaVoJAH4CsR1HMj3XCOF496XqiB7JoVBM/G/UluOI9WNFfmIMFzjW/2sHwRyqKnrhI6ISvJBMbpBxO2IV4pEZ/S/OdNraHCahVyhFQ9BHwT20vjbzp4u286VDeHe5wQaqZX+VQegQLAAbHzZAcWsrmVKgpMQkAYE2dqtwIDAQAB";

//商户在爱贝注册的应用ID对应的应用私钥
static NSString *mOrderUtilsCpPrivateKey = @"MIICWwIBAAKBgQC5J2xvBjK+AXJm+m8uYL63nzcM9BOjj4JGZBsLaIt0Kg2zlHzgi1wHnUqB3d+j3zwkdmx3qh47Yr0Y26PnlIaK2kCTsa09oWhl2gJoFAoVdiYtfmAiLOJ39FOpbpxfEcuJZzvKReFubs3ucRMBmZSgFIw9MPvBYpUvrB8+W+SkiQIDAQABAoGAVYm+Eai2Zot6k9Kc7LsrQN+gai6UqBqTn/t9dw7MZlVLUSyHaKFZWfczxb2AJU/rlBjV0Y7ZOqxKGiLWnRVD9jZzsTtFJX4LU/r35UmTa7vaYL/cx9ean3VT86a46rCxErEMDWgPqzwmWvAHUlRSDYmIBycNslTzWVNz0Bz9oIECQQDovqKWGTcO/w+jCUBs8VK3ht4k2obYmAdqCOu4Ji4k3da1/aXzBRQCh3Y9D39oJ3LNA6segBUB0yocJnEYLSkZAkEAy6d4DdXRbdGQiWEaC1HFnfnANWEvGUCQNsryygfjgy43hgWHsoo1NFSL/DlDnDi/Oq9mW5jXO3KQtluLyeMU8QJAA94PE5FtsckZOGtR771+01/hVxlufjDghqmJSTEROTmadMG3PTeLYpNfqpoUUuW86Z0y/bnBH5ujqn4VCQLPGQJAOqKEAU+/laPRvowTiJDJmftPaM8LVXTuUKhMEPkGez1yRQcQk8oRGXkCOONv4OnBvS7FBLhTt8z7d1WSQNU9sQJAB6FYHkN9mOUSq8JEVZS/2kHxaNy5q1ZTVHzcdUwpcld+Q646Le+rh2chM5AGK8Q+pVuQ3KIbcPdQtMihN8aoCg==";


- (void)initPayKit
{
    //初始化SDK信息
    //设置爱贝SDK的SSO回调
    NSString *appUrlScheme = @"scheme.com.iapppay.IapppayDemo";
    [[IapppayKit sharedInstance] setAppURLScheme:appUrlScheme];
    
    [[IapppayKit sharedInstance] setAppId:mOrderUtilsAppId ];
    [[IapppayKit sharedInstance]  setChannelId:mOrderUtilsChannel];
    
    // 设置屏幕方向
    UIInterfaceOrientationMask directionMask = UIInterfaceOrientationMaskLandscape;
    [[IapppayKit sharedInstance] setIapppayWindowDirectionMask:directionMask];
}

- (void)buyRequest:(NSString*)appuserid
                waresid:(NSString*)waresid
                orderid:(NSString*)orderid
{
    //@"MbtgAli20161118aV6jmd"  1000019
    //点击了想体验的商品类型的section
    OrderUtils *orderInfo = [[OrderUtils alloc] init];
    orderInfo.appId         = mOrderUtilsAppId;
    orderInfo.cpPrivateKey  = mOrderUtilsCpPrivateKey;
    orderInfo.notifyUrl     = mOrderUtilsNotifyurl;
    orderInfo.cpOrderId     = orderid;
    orderInfo.appUserId     = appuserid;
    orderInfo.waresId       = waresid;
    orderInfo.price         = @"0.1";
    orderInfo.waresName     = @"包月";
    orderInfo.cpPrivateInfo = @"商户包月商品的私有信息";
    NSString *trandInfo = [orderInfo getSignOrderData];
    //NSLog(@"generateTradeNO  : %@", orderid);
    //调用SDK方法，拉起SDK收银台的窗口进行支付
    [[IapppayKit sharedInstance] makePayForTrandInfo:trandInfo
                                   payResultDelegate:self];

}

- (void)buyRequest:(NSString*)trandInfo

{
    //NSLog(@"generateTradeNO  : %@", orderid);
    //调用SDK方法，拉起SDK收银台的窗口进行支付
    [[IapppayKit sharedInstance] makePayForTrandInfo:trandInfo
                                   payResultDelegate:self];
    
}


//此处方法是支付结果处理
#pragma mark - IapppayKitPayRetDelegate
- (void)iapppayKitRetPayStatusCode:(IapppayKitPayRetCode)statusCode
                        resultInfo:(NSDictionary *)resultInfo
{
    NSLog(@"statusCode : %d, resultInfo : %@", (int)statusCode, resultInfo);
    
    if (statusCode == IapppayKitPayRetSuccessCode)
    {
        NSString *message = @"";
        NSString *pkey = mOrderUtilsCheckResultKey;
        NSString *sign = [resultInfo objectForKey:@"Signature"];
        if ([OrderUtils verifyPayResult:sign publicKey:pkey])
        {
            //支付成功，验签成功
            message = @"支付成功，验签成功";
			UnitySendMessage("GameMgr", "IappayReqProductSucc",[message UTF8String]);
        }
        else
        {
            //支付成功，验签失败
            message = @"支付成功，验签失败";
			UnitySendMessage("GameMgr", "IappayReqProductSucc",[message UTF8String]);
        }
    }
    else if (statusCode == IapppayKitPayRetFailedCode)
    {
        //支付失败
        NSString *message = @"支付失败";
        if (resultInfo != nil) {
            message = [NSString stringWithFormat:@"%@:code:%@\n（%@）",message,resultInfo[@"RetCode"],resultInfo[@"ErrorMsg"]];
        }
        UnitySendMessage("GameMgr", "IappayReqProductFailed",[message UTF8String]);
//        [MBProgressHUD showTextHUDAddedTo:self.view Msg:message animated:YES];
    }
    else
    {
        //支付取消
        NSString *message = @"支付取消";
        if (resultInfo != nil) {
            message = [NSString stringWithFormat:@"%@:code:%@\n（%@）",message,resultInfo[@"RetCode"],resultInfo[@"ErrorMsg"]];
        }
		UnitySendMessage("GameMgr", "IappayReqProductFailed",[message UTF8String]);
//        [MBProgressHUD showTextHUDAddedTo:self.view Msg:message animated:YES];
    }
}


@end
 */