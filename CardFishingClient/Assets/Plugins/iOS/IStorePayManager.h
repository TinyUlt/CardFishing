//
//  IAPManager.h
//  Unity-iPhone
//
//  Created by MacMini on 14-5-16.
//
//

#import <Foundation/Foundation.h>
#import <StoreKit/StoreKit.h>

@interface IStorePayManager : NSObject<SKProductsRequestDelegate, SKPaymentTransactionObserver>{
    SKProduct *proUpgradeProduct;
    SKProductsRequest *productsRequest;
}

-(void)attachObserver;
-(BOOL)CanMakePayment;
-(void)destroy;
-(void)requestProductData:(NSString *)productIdentifiers;
-(void)buyRequest:(NSString *)productIdentifier;

@end
