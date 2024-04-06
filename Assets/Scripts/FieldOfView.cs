using System.Collections;
using System.Collections.Generic;
using Unity.BossRoom.Gameplay;
using Unity.BossRoom.Gameplay.GameplayObjects;
using Unity.BossRoom.Gameplay.GameplayObjects.Character;
using Unity.BossRoom.Gameplay.UserInput;
using UnityEngine;

namespace Unity.Multiplayer.Samples.BossRoom
{
    public class FieldOfView : MonoBehaviour
    {
        public List<GameObject> view = new List<GameObject>();
        public List<Vector3Int> pos = new List<Vector3Int>();

        public List<int> ImpHealth = new List<int>();

        //if an enemy enter in the player area (box collider), 
        //seve it in the view list and get his position and save in 
        //another list
        private void OnTriggerEnter(Collider other)
        {
            if ((other.CompareTag("Imp") || 
                other.CompareTag("Mini_Boss") || 
                    other.CompareTag("Boss")) && 
                        !view.Contains(other.gameObject))
            {
                //if the enemy not in view, add it
                
                view.Add(other.gameObject);


                Vector3Int tmp = new Vector3Int();
                float aux = other.transform.position.x * 100f;
                tmp.x = (int)aux;
                aux = other.transform.position.y * 100f;
                tmp.y = (int)aux;
                aux = other.transform.position.z * 100f;
                tmp.z = (int)aux;

                pos.Add(tmp);

                //enemy health list
                NetworkHealthState health = other.GetComponent<NetworkHealthState>();
                if(health != null){
                    ImpHealth.Add(health.HitPoints.Value);
                }
                
            }
        }
        
        //if an enemy leave the view area, remove it from all lists
        private void OnTriggerExit(Collider other)
        {
            if (view.Contains(other.gameObject))
            {
                
                int it = view.IndexOf(other.gameObject);

                view.Remove(other.gameObject);

                pos.RemoveAt(it);
                ImpHealth.RemoveAt(it);
                
            }
        }

        //on every update, if there is at least 1 enemy we update his data
        private void FixedUpdate()
        {
            GameObject obj = GameObject.Find("Observer");
            Info info = obj.GetComponent<Info>();

            if (view.Count != 0)
            {
                Vector3Int tmp = new Vector3Int();
                float aux;
                for (int it = 0; it < view.Count; it++)
                {
                    NetworkHealthState health=null;
                    if(view[it]!=null)
                        health = view[it].GetComponent<NetworkHealthState>();
                    
                    //if an enemy has 0 health and is still on the list, remove it 
                    if (view[it] == null || health.HitPoints.Value<=0)
                    {
                        
                        view.RemoveAt(it);
                        pos.RemoveAt(it);
                        ImpHealth.RemoveAt(it);
                    }
                    else // else, update his data
                    {
                        aux = view[it].transform.position.x * 100f;
                        tmp.x = (int)aux;
                        aux = view[it].transform.position.y * 100f;
                        tmp.y = (int)aux;
                        aux = view[it].transform.position.z * 100f;
                        tmp.z = (int)aux;
                        pos[it] = tmp;

                        if(health != null && health.HitPoints.Value!=ImpHealth[it]){
                            ImpHealth[it]=health.HitPoints.Value;
                        }
                    }
                }
                
            }
            
        }

        //get enemy in the view list at "index" position
        public GameObject getAt(int index)
        {
            if(index < view.Count && view[index]!=null)
                return view[index];
            return null;
        }
    }
}
