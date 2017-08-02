using UnityEngine;

public abstract class Singletion<T> where T : new() {  

	protected static T m_instance;  
	public static T Instance  
	{  
		get  
		{  
			if (m_instance == null)  
			{  
				m_instance = new T();  
			}  
			return m_instance;  
		}  
	}  

}

public abstract class MonoSingletion<T> : MonoBehaviour where T : MonoBehaviour {   

	protected static T m_instance;  
	public static T Instance  
	{  
		get  
		{  
			if (m_instance == null)
			{
				m_instance = FindObjectOfType<T>();
				if(m_instance == null){
					GameObject go = new GameObject (typeof(T).Name);
					go.AddComponent <T>();
                    m_instance = FindObjectOfType<T>();
                }
			}
			return m_instance;
		}  
	}  

}
