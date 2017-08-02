//
//  UJSInterface.m
//  Unity-iPhone
//
//  Created by MacMini on 14-5-15.
//
//

#import "IStorePayInterface.h"
#import "IStorePayManager.h"

@implementation IStorePayInterface

IStorePayManager *istoreManager = nil;

void _InitIStoreManager(){
    istoreManager = [[IStorePayManager alloc] init];
    [istoreManager attachObserver];
    
}

bool _IStoreIsProductAvailable(){
    return [istoreManager CanMakePayment];
}

void _IStoreRequstProductInfo(void *p){
    NSString *list = [NSString stringWithUTF8String:p];
    NSLog(@"productKey:%@",list);
    [istoreManager requestProductData:list];
}

void _IStoreBuyProduct(void *p){
    [istoreManager buyRequest:[NSString stringWithUTF8String:p]];
}

void _IStoreDestroy()
{
    [istoreManager destroy];
	istoreManager = nil;
}

@end
