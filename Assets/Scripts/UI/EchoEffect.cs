using UnityEngine;

namespace DungTran31.UI
{
    public class EchoEffect : MonoBehaviour
    {
        private float timeBtwSpawns;
        [SerializeField] private float startTimeBtwSpawns;
        [SerializeField] private GameObject echo;


        private void Update()
        {
            if(timeBtwSpawns <= 0)
            {
                GameObject instance = Instantiate(echo, transform.position, Quaternion.identity);
                Destroy(instance, 7f);
                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }    
        }
    }
}
