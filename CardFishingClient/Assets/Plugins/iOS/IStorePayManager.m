//
//  IAPManager.m
//  Unity-iPhone
//
//  Created by MacMini on 14-5-16.
//
//

#import "IStorePayManager.h"


@implementation IStorePayManager


NSArray *products;
-(void) attachObserver{
    NSLog(@"AttachObserver");
    [[SKPaymentQueue defaultQueue] addTransactionObserver:self];
}

-(BOOL) CanMakePayment{
    return [SKPaymentQueue canMakePayments];
}

//请求商品列表
-(void) requestProductData:(NSString *)productIdentifiers{
    NSArray *idArray = [productIdentifiers componentsSeparatedByString:@"\t"];
    NSSet *idSet = [NSSet setWithArray:idArray];
    [self sendRequestProductData:idSet];
}

-(void)sendRequestProductData:(NSSet *)idSet{
    SKProductsRequest *request = [[SKProductsRequest alloc] initWithProductIdentifiers:idSet];
    request.delegate = self;
    [request start];
}

//获取商品列表回调
-(void)productsRequest:(SKProductsRequest *)request didReceiveResponse:(SKProductsResponse *)response{
    products = response.products;
    
    for(NSString *invalidProductId in response.invalidProductIdentifiers){
        NSLog(@"Invalid product id:%@",invalidProductId);
    }
    UnitySendMessage("GameMgr", "IStoreReqProductListSucc","");
}

-(void)destroy
{
    products = nil;
    [[SKPaymentQueue defaultQueue] removeTransactionObserver:self];
}


//购买商品
-(void)buyRequest:(NSString *)productIdentifier{
    if([products count] != 0)
    {
        for (SKProduct *p in products) {
            if([p.productIdentifier isEqualToString:productIdentifier])
            {
				UnitySendMessage("GameMgr", "IStoreProductIdRet", [productIdentifier UTF8String]);
                SKPayment *payment = [SKPayment paymentWithProduct:p]; //]:productIdentifier];
                [[SKPaymentQueue defaultQueue] addPayment:payment];
                return;
            }
            
        }
    }
	UnitySendMessage("GameMgr", "IStoreReqProductFailed","购买的商品不存在");
}
//商品列表转成字符串列表
-(NSString *)productListToString:(SKProduct *)product{
    NSArray *info = [NSArray arrayWithObjects:product.localizedTitle,product.localizedDescription,product.price,product.productIdentifier, nil];
    
    return [info componentsJoinedByString:@"\t"];
}

-(NSString *)transactionInfo:(SKPaymentTransaction *)transaction{
    
    return [self encode:(uint8_t *)transaction.transactionReceipt.bytes length:transaction.transactionReceipt.length];
    
    //return [[NSString alloc] initWithData:transaction.transactionReceipt encoding:NSASCIIStringEncoding];
}

-(NSString *)encode:(const uint8_t *)input length:(NSInteger) length{
    static char table[] = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    
    NSMutableData *data = [NSMutableData dataWithLength:((length+2)/3)*4];
    uint8_t *output = (uint8_t *)data.mutableBytes;
    
    for(NSInteger i=0; i<length; i+=3){
        NSInteger value = 0;
        for (NSInteger j= i; j<(i+3); j++) {
            value<<=8;
            
            if(j<length){
                value |=(0xff & input[j]);
            }
        }
        
        NSInteger index = (i/3)*4;
        output[index + 0] = table[(value>>18) & 0x3f];
        output[index + 1] = table[(value>>12) & 0x3f];
        output[index + 2] = (i+1)<length ? table[(value>>6) & 0x3f] : '=';
        output[index + 3] = (i+2)<length ? table[(value>>0) & 0x3f] : '=';
    }
    
    return [[NSString alloc] initWithData:data encoding:NSASCIIStringEncoding];
}

-(void) provideContent:(SKPaymentTransaction *)transaction{
    //UnitySendMessage("Main", "ProvideContent", [[self transactionInfo:transaction] UTF8String]);
}

-(void)paymentQueue:(SKPaymentQueue *)queue updatedTransactions:(NSArray *)transactions{
    for (SKPaymentTransaction *transaction in transactions) {
        switch (transaction.transactionState) {
            case SKPaymentTransactionStatePurchased:
                [self completeTransaction:transaction];
                break;
            case SKPaymentTransactionStateFailed:
                [self failedTransaction:transaction];
                break;
            case SKPaymentTransactionStateRestored:
                [self restoreTransaction:transaction];
                break;
            default:
                break;
        }
    }
}
//支付成功
-(void) completeTransaction:(SKPaymentTransaction *)transaction{
    NSLog(@"Comblete transaction : %@",transaction.transactionIdentifier);
    //[self provideContent:transaction];

	char* tmp = [transaction.transactionReceipt.base64Encoding cStringUsingEncoding:NSASCIIStringEncoding];
    //NSString *jsonObjectString = [self encode:(uint8_t *)transaction.transactionReceipt.bytes length:transaction.transactionReceipt.length];
    UnitySendMessage("GameMgr", "IStoreReqProductSucc",tmp);
    NSLog(@"交易结束");
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}
//支付失败
-(void) failedTransaction:(SKPaymentTransaction *)transaction{
    NSLog(@"Failed transaction : %@",transaction.transactionIdentifier);
    
	//NSLog(@"!failed  %@",  [NSString stringWithUTF8String:transaction.error.code]);
	NSString *detail = @"";
    if (transaction.error != nil) {  
    switch (transaction.error.code) {  
              
        case SKErrorUnknown:  
              
            NSLog(@"SKErrorUnknown");  
            detail = @"未知错误";  
            break;  
              
        case SKErrorClientInvalid:  
              
            NSLog(@"SKErrorClientInvalid");  
            detail = @"当前苹果账户无法购买商品";  
            break;  
              
        case SKErrorPaymentCancelled:  
              
            NSLog(@"SKErrorPaymentCancelled");  
            detail = @"订单已取消";  
            break;  
        case SKErrorPaymentInvalid:  
            NSLog(@"SKErrorPaymentInvalid");  
            detail = @"订单无效";  
            break;  
              
        case SKErrorPaymentNotAllowed:  
            NSLog(@"SKErrorPaymentNotAllowed");  
            detail = @"当前苹果设备无法购买商品";  
            break;  
              
        case SKErrorStoreProductNotAvailable:  
            NSLog(@"SKErrorStoreProductNotAvailable");  
            detail = @"当前商品不可用";  
            break;  
              
        default:  
              
            NSLog(@"No Match Found for error");  
            detail = @"未知错误";  
            break;  
    } 
	}	
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
	UnitySendMessage("GameMgr", "IStoreReqProductFailed",[detail UTF8String]);
}

-(void) restoreTransaction:(SKPaymentTransaction *)transaction{
    NSLog(@"Restore transaction : %@",transaction.transactionIdentifier);
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}


@end
