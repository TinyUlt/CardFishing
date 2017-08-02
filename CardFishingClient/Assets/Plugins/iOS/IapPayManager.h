/* //
//  IAPManager.h
//  Unity-iPhone
//
//  Created by MacMini on 14-5-16.
//
//

#import <Foundation/Foundation.h>
#import <StoreKit/StoreKit.h>

@interface IapPayManager : NSObject
-(void)initPayKit;
- (void)buyRequest:(NSString*)appuserid
           waresid:(NSString*)waresid
           orderid:(NSString*)orderid;
- (void)buyRequest:(NSString*)trandInfo;
@end
 */