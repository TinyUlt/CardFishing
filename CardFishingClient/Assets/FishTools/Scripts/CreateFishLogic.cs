using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using System.IO;
using GtMsg;


#if UNITY_EDITOR 
using UnityEditor;
#endif
public class CreateFishLogic : MonoBehaviour {

//	//结果提示
//	public Text Result;
//	//保存文件
//	public InputField OriginFile;
//	public InputField SaveFile;
//	public GameObject PathScrollView;
//	//滚动内容
//	public GameObject ListContent;
//	//
//	public GameObject PathContent;

//	public GameObject OriginContent;
//	//
//	public GameObject ProductListItem;
//	//
//	public GameObject ProductPathItem;
//	//内容的原始高度
//	public Vector2 ListContentSize;
//	public Vector2 PathContentSize;
//	public Vector2 OriginContentSize;
//	//项高
//	private float ListItemHeight;
//	private float PathItemHeight;
//	private float OriginItemHeight;

//	//存放项到容器
//	//public List<GameObject> ListItems;
//	//存放项到容器
//	//public List<GameObject> PathItems;

//	public InputField ItemTitleName;
//	public InputField ItemTitleTime;
//	public InputField GetNamsInputFiles;

//	private Toggle CurrentListToggle;
//	private Toggle CurrentPathToggle;
//	bool isCollapse;
//	//private  ProduectItemGroup ProductItemStore;
//	//文件名
//	//string fileName;
//	//string originFile;
//	void Start(){

//		var originFile = PlayerPrefs.GetString ("OriginPathFile_Client");

//		if (originFile == "") {

//			originFile = "OriginPathFile_Client";
//		}

//		OriginFile.text = originFile;



//		var fileName = PlayerPrefs.GetString ("ProductLogic");

//		if (fileName == "") {

//			fileName = "ProductLogic";
//		}

//		SaveFile.text = fileName;


//		ListContentSize = ListContent.GetComponent<RectTransform>().sizeDelta;

//		ListItemHeight = ListContent.GetComponent<GridLayoutGroup> ().cellSize.y;

//		PathContentSize = PathContent.GetComponent<RectTransform>().sizeDelta;

//		PathItemHeight = PathContent.GetComponent<GridLayoutGroup> ().cellSize.y;

//		OriginContentSize = OriginContent.GetComponent<RectTransform>().sizeDelta;

//		OriginItemHeight = OriginContent.GetComponent<GridLayoutGroup> ().cellSize.y;

//		LoadOrigin ();


//	}
//	//重置列表大小
//	void ResizeContent(){


//		PathContent.GetComponent<RectTransform>().sizeDelta = new Vector2(PathContentSize.x, PathContent.transform.childCount * PathItemHeight + PathContentSize.y);
//		ListContent.GetComponent<RectTransform>().sizeDelta = new Vector2(ListContentSize.x, (ListContent.transform.childCount+1) * ListItemHeight + ListContentSize.y);

//	}
//	public void SetNames(){

//		var name = GetNames ();

//		if(CurrentPathToggle != null){
			
//			CurrentPathToggle.transform.FindChild ("PathNameInputField").GetComponent<InputField> ().text = name;
//		}
//		SavePathContent ();
//	}
//	public void AddNames (){

//		var name = GetNames ();

//		if (CurrentListToggle != null) {

//			string[] s = name.Split(new char[] { ' ' });

//			foreach (var c in s) {

//				var obj = AddProductPathItem ();

//				obj.transform.FindChild ("PathNameInputField").GetComponent<InputField> ().text = c;
//			}
//		}
//		SavePathContent ();
//	}
//	string  GetNames(){

//		string ANames = GetNamsInputFiles.text;

//		string BNames = ANames.Replace ("*", "");

//		string CNames = "";

//		if (BNames != ANames) {

//			foreach (var item in OriginContent.GetComponentsInChildren<ProductUnit>()) {

//				string DName = item.productItem.ProductName;

//				if (DName.IndexOf (BNames) != -1) {

//					if (CNames.Length != 0) {
						
//						CNames += " ";
//					}

//					CNames += DName;


//				};
//			}

//			foreach (var item in ListContent.GetComponentsInChildren<ProductUnit>()) {

//				string DName = item.productItem.ProductName;

//				if (DName.IndexOf (BNames) != -1) {

//					if (CNames.Length != 0) {
						
//						CNames += " ";
//					}

//					CNames += DName;


//				};
//			}
//		} else {

//			CNames = BNames;
//		}

//		return CNames;
//	}

//	//添加列表项回调
//	public void AddProductListItemCallback(){
		
//		var toggle = AddProductUnitItem (ListContent);

//		toggle.onValueChanged.AddListener((bool value) => OnListToggleClick(toggle, value));

//		ProductItem productItem = new ProductItem ();

//		productItem.ProductName = toggle.GetComponentInChildren<InputField> ().text;

//		productItem.Time = 1;

//		SetUnitItemProduct (toggle, productItem);
//	}
////	public void AddProductOriginItemCallback(){
////
////		var toggle = AddProductUnitItem (OriginContent);
////
////		toggle.GetComponentInChildren<InputField> ().text = toggle.GetComponentInChildren<InputField> ().text;
////	}
//	//添加列表项
//	public Toggle AddProductUnitItem(GameObject content){

//		GameObject item = Instantiate (ProductListItem ) as GameObject ;

//		item.transform.parent = content.transform;

//		item.transform.localScale = Vector3.one;

//		//ListItems.Add (item);


//		var toggle = item.GetComponent<Toggle> ();

//		toggle.group = content.GetComponent<ToggleGroup> ();



//		ResizeContent ();

//		return toggle;
//	}

//	public void AddProductPathItemCallback(){

//		AddProductPathItem ();
//	}
//	//添加一个Path项
//	public GameObject AddProductPathItem(){

//		GameObject item = Instantiate (ProductPathItem ) as GameObject ;

//		item.transform.parent = PathContent.transform;

//		item.transform.localScale = Vector3.one;

//		var toggle = item.GetComponent<Toggle> ();
//		toggle.group = PathContent.GetComponent<ToggleGroup> ();

//		//PathItems.Add (item);
//		toggle.onValueChanged.AddListener((bool value) => OnPathToggleClick(toggle, value));

//		ResizeContent ();

//		return item;
//	}
//	void ShowPathItem(Toggle toggle){

//		DeleteAllPathItem ();
//		isCollapse = false;
	
//		var productItem = toggle.GetComponent<ProductUnit> ().productItem;

//		if (productItem == null) {
//			return;
//		}

//		ItemTitleName.text = productItem.ProductName;

//		ItemTitleTime.text = productItem.Time.ToString ();

//		foreach (var item in productItem.ContentItems) {

//			var frame = item.Frame;

//			string str = "";

//			for (var i =0; i < item.Items.Count; i++) {

//				if (i != 0) {

//					str += " ";
//				}
//				str += item.Items[i].ProductName;
//			}
//			//var pichild = item.Items [0];

//			var obj = AddProductPathItem ();

//			obj.transform.FindChild ("PathNameInputField").GetComponent<InputField> ().text = str;
//			obj.transform.FindChild ("FrameInputField").GetComponent<InputField> ().text = frame.ToString ();
//			obj.transform.FindChild ("TypeInputField").GetComponent<InputField> ().text = item.FishType;
//		}
//	}

//	public  void ShowCollapseItemCallback(){

//		if (isCollapse == false) {
			
//			//SavePathContent ();

//			DeleteAllPathItem ();

//			var toggle = CurrentListToggle;

//			var productItem = toggle.GetComponent<ProductUnit> ().productItem;

//			if (productItem == null) {
//				return;
//			}


//			ItemTitleName.text = productItem.ProductName;

//			ItemTitleTime.text = productItem.Time.ToString ();

//			int frame = 0;

//			ShowCollapseItem (productItem,ref frame, "Main");

//			isCollapse = true;

//		} else {
			
//			DeleteAllPathItem ();

//			ShowPathItem (CurrentListToggle);
//		}
//	}
//	void RelateProduct(ProductItem productItem){

//		for(var j = 0; j < productItem.ContentItems.Count; j++){

//			var item = productItem.ContentItems [j];

//			for (var i = 0; i < item.Items.Count; i++) {
			
//				RelateProduct (item.Items[i]);

//				var p = FindProductItemByName (item.Items [i].ProductName);

//				if (p != null) {

//					item.Items [i] = p;
//				}
//			}

//		}
		
//	}
//	void ShowCollapseItem(ProductItem productItem, ref int frame, string type){

//		if (productItem == null) {
//			return;
//		}

//		if (productItem.ContentItems.Count == 0) {
			
//			var obj = AddProductPathItem ();

//			obj.transform.FindChild ("PathNameInputField").GetComponent<InputField> ().text = productItem.ProductName;

//			obj.transform.FindChild ("FrameInputField").GetComponent<InputField> ().text = (frame).ToString();

//			obj.transform.FindChild ("TypeInputField").GetComponent<InputField> ().text = type;
//		}
//		for (var i = 0; i < productItem.Time; i++) {

//			for(var j = 0; j < productItem.ContentItems.Count; j++){
				
//				var item = productItem.ContentItems [j];

//				frame = item.Frame + frame;

//				string t = item.FishType;

//				if (item.FishType == "") {

//					t = type;
//				}

//				ShowCollapseItem (item.Items [Random.Range(0,item.Items.Count)],ref frame, t);
//			}
//		}
//	}

//	public void OnListToggleClick(Toggle toggle, bool value)
//	{
//		if (value) {
//			//SavePathContent ();
//			SavePathContent();

//			CurrentListToggle = toggle;

//			PathScrollView.SetActive (true);
//			ShowPathItem (toggle);

//			//SavePathContent ();
//		}
//	}

//	public void OnPathToggleClick(Toggle toggle, bool value)
//	{
//		if (value) {

//			CurrentPathToggle = toggle;
//		}
//	}
//	//设置ProductItem 到标签中
//	void SetUnitItemProduct(Toggle toggle, ProductItem productItem){

//		toggle.GetComponent<ProductUnit> ().productItem = productItem;

//		toggle.GetComponentInChildren<InputField> ().text = productItem.ProductName;
//	}

//	public List<ProductItem> GetProductListByName(string name){

//		string[] s = name.Split(new char[] { ' ' });

//		List<ProductItem> pl = new List<ProductItem> ();
//		foreach (var c in s) {

//			var pi = FindProductItemByName (c);

//			//if (pi != null) {

//			pl.Add (pi);

//			if (pi == null) {

//				Result.text = "找不到路径:" + c;
//			}
//			//}
//		}

//		return pl;
//	}

//	Toggle FindToggleByName(string name){

//		for(int i = 0;i<ListContent.transform.childCount;i++)
//		{
//			GameObject go = ListContent.transform.GetChild(i).gameObject;

//			if (go.GetComponentInChildren<InputField> ().text == name) {
			
//				return go.GetComponent<Toggle> ();
//			}
//		}

//		return null;
//	}
//	public bool ContainNameInOriginContent(string name){

//		foreach (var item in OriginContent.GetComponentsInChildren<ProductUnit>()) {

//			if (item.productItem.ProductName == name) {

//				return true;
//			}
//		}

//		return false;
//	}
//	public ProductItem FindProductItemByName(string name){

//		foreach (var item in OriginContent.GetComponentsInChildren<ProductUnit>()) {

//			if (item.productItem.ProductName == name) {

//				return item.productItem;
//			}
//		}

//		foreach (var item in ListContent.GetComponentsInChildren<ProductUnit>()) {

//			if (item.productItem.ProductName == name) {

//				return item.productItem;
//			}
//		}

//		return null;
//	}

//	ProductItem SetProductItemFromPathContent(ProductItem productItem){
		
//		//ProductItem productItem = new ProductItem ();

////		if (productItem == null) {
////
////			productItem = new ProductItem ();
////		}
//		productItem.ContentItems.Clear ();

//		productItem.ProductName = ItemTitleName.text;

//		productItem.Time = int.Parse (ItemTitleTime.text);

//		bool saveOk = true;

//		//var PathItems = PathContent.
//		for (var i = 0; i < PathContent.transform.childCount; i++) {

//			var item = PathContent.transform.GetChild (i).gameObject;

//			ProductItemContent pc = new ProductItemContent ();

//			var pathName =  (item.transform.FindChild ("PathNameInputField").GetComponent<InputField> ().text);

//			pc.Frame = int.Parse (item.transform.FindChild ("FrameInputField").GetComponent<InputField> ().text);

//			pc.FishType =  (item.transform.FindChild ("TypeInputField").GetComponent<InputField> ().text);

//			if (pathName == "Escape" || pathName == "YuZhen") {

//				ProductItem p = new ProductItem ();

//				p.ProductName = pathName;

//				pc.Items.Add (p);
			

//			} else {

//				var findPList = GetProductListByName (pathName);

//				foreach (var findP in findPList) {

//					if (findP == null) {

//						saveOk = false;

//						continue;
//					}
//					pc.Items.Add (findP);
//				}
//			}
//			productItem.ContentItems.Add (pc);


//		}

//		if(saveOk){
			
//			Result.text = "添加成功";
//		}
//		return productItem;
//	}
//	void DeleteAllOriginItem(){
//		List<GameObject> list = new List<GameObject> ();

//		for(int i = 0;i<OriginContent.transform.childCount;i++)
//		{
//			GameObject go = OriginContent.transform.GetChild(i).gameObject;

//			list.Add (go);
//		}

//		foreach (var item in list) {

//			item.transform.parent = null;

//			Destroy(item);
//		}
//	}
//	void DeleteAllPathItem(){

//		List<GameObject> list = new List<GameObject> ();

//		for(int i = 0;i<PathContent.transform.childCount;i++)
//		{
//			GameObject go = PathContent.transform.GetChild(i).gameObject;

//			list.Add (go);
//		}

//		foreach (var item in list) {
			
//			item.transform.parent = null;

//			Destroy(item);
//		}
//	}
//	void DeleteAllListItem(){
		
//		List<GameObject> list = new List<GameObject> ();

//		for(int i = 0;i<ListContent.transform.childCount;i++)
//		{
//			GameObject go = ListContent.transform.GetChild(i).gameObject;

//			list.Add (go);
//		}
//		foreach (var item in list) {

//			item.transform.parent = null;

//			Destroy(item);
//		}
//	}
//	public void DeleteListItem(){

//		if (CurrentListToggle != null) {

//			CurrentListToggle.gameObject.transform.parent = null;

//			Destroy (CurrentListToggle.gameObject);

//			ResizeContent ();

//			PathScrollView.SetActive (false);

//			CurrentListToggle = null;

//			if (ListContent.transform.childCount > 0) {

//				ListContent.transform.GetChild(ListContent.transform.childCount-1).gameObject.GetComponent<Toggle>().isOn = true;
//			}

//		}
//	}
//	public void DeletePathItem(){
	
//		if (CurrentPathToggle != null) {
			
//			CurrentPathToggle.gameObject.transform.parent = null;

//			Destroy (CurrentPathToggle.gameObject);

//			ResizeContent ();

//			CurrentPathToggle = null;

//			if (PathContent.transform.childCount > 0) {

//				PathContent.transform.GetChild(PathContent.transform.childCount-1).gameObject.GetComponent<Toggle>().isOn = true;
//			}
//		}
//	}
//	public void UpPathItem(){
//		UpItem (CurrentPathToggle);
//	}
//	public void DownPathItem(){
//		DownItem (CurrentPathToggle);
//	}
//	public void UpListItem(){

//		UpItem (CurrentListToggle);
//	}
//	public void DownListItem(){

//		DownItem (CurrentListToggle);
//	}
//	public void UpItem(Toggle toggle){

//		if (toggle) {

//			toggle.transform.SetSiblingIndex( toggle.transform.GetSiblingIndex () -1);
//		}

//	}
//	public void DownItem(Toggle toggle){
		
//		if (toggle) {

//			toggle.transform.SetSiblingIndex( toggle.transform.GetSiblingIndex () +1);
//		}
//	}


//	public void SavePathContent(){

//		if (CurrentListToggle != null) {

//			if (isCollapse == true) {

//				ShowCollapseItemCallback ();
//			}


//			var productItem = CurrentListToggle.GetComponentInChildren<ProductUnit> ().productItem;

//			SetProductItemFromPathContent(productItem);

//			//productItem = productItem;

//			CurrentListToggle.GetComponentInChildren<InputField> ().text = productItem.ProductName;
//		}
//	}
//	public void SaveToFile(){

//		if (isCollapse == true) {

//			ShowCollapseItemCallback ();
//		}

//		SavePathContent ();

//		var nameList = new  Dictionary<string, int> ();

//		foreach (var item in ListContent.GetComponentsInChildren<InputField>()) {
		
//			if (nameList.ContainsKey (item.text)) {

//				Result.text = "错误: " + item.text+ " 不能重名";

//				return;
//			}

//			nameList [item.text] =1 ;


//		}

//		var ProductItemStore = new ProductItemGroup();

//		foreach (var item in OriginContent.GetComponentsInChildren<ProductUnit>()) {

//			ProductItemStore.ItemList.Add (item.productItem);
//		}

//		foreach (var item in ListContent.GetComponentsInChildren<ProductUnit>()) {

//			if (item.productItem.ContentItems.Count == 0) {

//				Result.text =item.productItem.ProductName+ " 路径不能为空";

//				return;
//			}
//			ProductItemStore.ItemList.Add (item.productItem);
//		}

////		var oldToggle = CurrentListToggle;
////
////		var toggle = FindToggleByName ("Main");
////
////		if (toggle == null) {
////
////			Result.text = "失败，必须存在Main路径";
////
////			return;
////		}
////		toggle.isOn = true;
////
////		ShowFrameItemCallback ();
////
////		for(int i = 0;i<PathContent.transform.childCount;i++)
////		{
////			PathQueue PQ = new PathQueue ();
////			
////			GameObject go = PathContent.transform.GetChild(i).gameObject;
////
////			PQ.PathName = go.transform.FindChild ("PathNameInputField").GetComponent<InputField> ().text;
////
////			PQ.Frame = int.Parse (go.transform.FindChild ("FrameInputField").GetComponent<InputField> ().text);
////
////			ProductItemStore.PathList.Add (PQ);
////
////		}

//		var fileName = SaveFile.text;

//		using (var output = File.Create(Application.dataPath+"/Resources/File/" + fileName + ".bytes"))
//		{
//			ProductItemStore.WriteTo(output);

//			//AssetDatabase.Refresh ();
//		}

//		//oldToggle.isOn = true;

//		Result.text = "保存成功";

//		PlayerPrefs.SetString("ProductLogic", fileName);
//	}

//	public void LoadOrigin(){

//		DeleteAllOriginItem ();

//		DeleteAllListItem ();

//		string fileName = OriginFile.text;
//		if (true/*File.Exists (Application.dataPath+"/ProjectAssets/Resources/File/" + fileName + ".bytes")*/) {
			
//			PathGroupClient pg = new PathGroupClient ();

//			//using (var input = File.OpenRead(Common.dataPath +"/"+ fileName + ".json"))
//			TextAsset t = Resources.Load ("File/"+ fileName) as TextAsset;

//			if(t != null){
				
//					pg = PathGroupClient.Parser.ParseFrom(t.bytes);
//			}


//			foreach (var path in pg.PathList) {

//				ProductItem productItem = new ProductItem ();

//				productItem.ProductName = path.PathName;

//				productItem.Time = 1;

//				var toggle = AddProductUnitItem (OriginContent);

//				SetUnitItemProduct (toggle, productItem);

//			}
//			OriginContent.GetComponent<RectTransform>().sizeDelta = new Vector2(OriginContentSize.x, (pg.PathList.Count+1) * OriginItemHeight + OriginContentSize.y);

//			PlayerPrefs.SetString("OriginPathFile_Client", fileName);


//			LoadFile ();
//		}else {

//			Result.text = "找不到" + fileName + "文件";
//		}

//	}
//	public void LoadFile(){

//		DeleteAllListItem ();

//		PathScrollView.SetActive (false);

//		var ProductItemStore = new ProductItemGroup();

//		var fileName = SaveFile.text;

//		if (true/*File.Exists (Application.dataPath+"/ProjectAssets/Resources/File/" + fileName + ".bytes"*/) {

//			//using (var input = File.OpenRead (Common.dataPath + "/" + fileName + ".json")) {
//			TextAsset t = Resources.Load ("File/"+ fileName) as TextAsset;

//			if(t != null){
				
//				ProductItemStore = ProductItemGroup.Parser.ParseFrom (t.bytes);

//				foreach (var item in ProductItemStore.ItemList) {

//					if (item.ContentItems.Count > 0) {

//						var toggle = AddProductUnitItem (ListContent);

//						toggle.onValueChanged.AddListener((bool value) => OnListToggleClick(toggle, value));

//						SetUnitItemProduct (toggle, item);

//					}
//				}

//				foreach (var item in ListContent.GetComponentsInChildren<ProductUnit>()) {

//					RelateProduct (item.productItem);
//				}
//				if (ListContent.transform.childCount == 0) {
					
//					var toggle = AddProductUnitItem (ListContent);

//					toggle.onValueChanged.AddListener((bool value) => OnListToggleClick(toggle, value));

//					ProductItem productItem = new ProductItem ();

//					productItem.ProductName = "Main";

//					productItem.Time = 1;

//					SetUnitItemProduct (toggle, productItem);
//				}
//				//ListContent.transform.GetChild (0).GetComponent<Toggle> ().isOn = true;

//				Result.text = "加载成功";

//				PlayerPrefs.SetString("ProductLogic", fileName);
//			}
//		} else {

//			Result.text = "找不到" + fileName + "文件";
//		}
//	}
//	void OnDestroy(){
//		#if UNITY_EDITOR 
//		AssetDatabase.Refresh ();
//		#endif
//	}
}
