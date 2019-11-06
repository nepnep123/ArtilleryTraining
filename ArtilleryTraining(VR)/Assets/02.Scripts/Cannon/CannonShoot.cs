using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShoot : MonoBehaviour
{
    public static CannonShoot instance_CannonShoot;
    public Transform bulletPrefab;
    private Transform bullet;
    public GameObject explo_endPos;

    private float tx;
    private float ty;
    private float tz;
    private float v;
    public float g = 9.8f;
    public float max_height = 10.0f;

    private float elapsed_time;
    private float t;

    // public Transform start_pos;
    // public Transform end_pos;

    private float dat;  //도착점 도달 시간 

    // Start is called before the first frame update
    void Start()
    {
        //Shoot(max_height);
    }
    void Awake()
    {
        instance_CannonShoot = this;
    }

    public void Shoot(float _max_height, Transform _bullet, Transform _startPos, Transform _endPos)
    {
        this.max_height = _max_height;

        bullet = _bullet; //CannonCtrl에서 생성된 bullet;
        bullet.position = _startPos.position;

        Vector3 startPos = _startPos.position;
        Vector3 endPos = _endPos.position;

        var dh = endPos.y - startPos.y;
        var mh = max_height - startPos.y;
        ty = Mathf.Sqrt(2 * this.g * mh);   //

        float a = this.g;
        float b = -2 * ty;
        float c = 2 * dh;

        dat = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);

        tx = -(startPos.x - endPos.x) / dat; //
        tz = -(startPos.z - endPos.z) / dat; //

        this.elapsed_time = 0;

        //StartCoroutine(ShootImpl());
    }
    
    //포탄날라가는 궤도를 그리는 로직 
    public IEnumerator ShootImpl(Transform _bullet, Transform _startPos, Transform _endPos)
    {
        Vector3 startPos = _startPos.position;
        Vector3 endPos = _endPos.position;

        while (true)
        {
            this.elapsed_time += Time.deltaTime;
            var tx = startPos.x + this.tx * elapsed_time;
            var ty = startPos.y + this.ty * elapsed_time - 0.5f * g * elapsed_time * elapsed_time;
            var tz = startPos.z + this.tz * elapsed_time;
            var tpos = new Vector3(tx, ty, tz);

            GameObject _explo = Instantiate(explo_endPos, tpos, Quaternion.identity);
            Destroy(_explo, 0.5f);

            _bullet.transform.LookAt(tpos);
            _bullet.transform.position = tpos;

            if (this.elapsed_time >= this.dat)
                break;

            yield return null;
        }
    }

}
