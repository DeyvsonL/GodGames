using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class PlayerTrigger : NetworkBehaviour{

	public float bulletSpeed;

	private Player player;
	private Rigidbody body;
    private GameObject auxGancho;

    [SerializeField]
    private Transform rightHand;

	private LineRenderer lrMark;
	private GameObject mark;
	private LineRenderer lrGancho;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
	private GameObject bulletStunPrefab;
    [SerializeField]
	private GameObject hookPrefab;
    [SerializeField]
	private GameObject pillarPrefab;
	[SerializeField]
	private GameObject pillarExplosivePrefab;
    [SerializeField]
	private GameObject trapSlowPrefab;
    [SerializeField]
	private GameObject trapDamagePrefab;
    [SerializeField]
    private GameObject trapStunPrefab;

    public AudioClip soundShotOne;
    public float volSoundShotOne;
    public AudioClip soundShotTwo;
    public float volSoundShotTwo;
    public AudioClip soundShotHook;
    public float volSoundShotHook;
    public AudioClip soundErrorLowMana;
    public float volSoundErrorLowMana;
    public AudioClip soundShoutDamageArea;
    public float volSoundShoutDamageArea;
    public AudioClip soundFlameDamageArea;
    public float volSoundFlameDamageArea;
    private AudioSource source;

    public Button[] gameObjectsSkill;

    public int skill;
    private readonly int BULLET = 1;
    private readonly int PILLAR = 2;
    private readonly int HOOK = 3;
	private readonly int MARK = 4;
    private readonly int TRAPSLOW = 5;
    private readonly int TRAPDAMAGE = 6;
    private readonly int TRAPSTUN = 7;
    
    public int Skill
    {
        get { return Skill; }
    }

    public void alterSkill(int newSkill)
    {
        skill = newSkill;
    }

    private Animator anim;

    void Start(){
        source = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody> ();
        anim = GetComponentInChildren<Animator>();

        player = GetComponent<Player>();
        lrMark = GetComponent<LineRenderer>();
        lrMark.SetWidth(0.05f, 0.05f);
        lrMark.SetColors(Color.red, Color.red);
        mark = GameObject.FindGameObjectWithTag("mark");
        skill = 1;
        GameObject[] go = GameObject.FindGameObjectsWithTag("BtnSkill");
        gameObjectsSkill = new Button[go.Length];
        for (int i = 0; i<go.Length; i++)
        {
            gameObjectsSkill[i] = go[i].GetComponent<Button>();
        }

        for (int i = 0; i < gameObjectsSkill.Length-1; i++)
        {
            for(int j=i+1;j< gameObjectsSkill.Length; j++)
            {
                if (string.Compare( gameObjectsSkill[j].gameObject.name,  gameObjectsSkill[i].gameObject.name, false) < 0)
                {
                    Button goAux = gameObjectsSkill[j];
                    gameObjectsSkill[j] = gameObjectsSkill[i];
                    gameObjectsSkill[i] = goAux;

                }
                
            }
        }
    }

    void Update() {
        if (!isLocalPlayer || player.Dead)
            return;

        selectSkill();

        RaycastHit hit;
        Vector3 realDirection;
        calculateDirection(out hit, out realDirection);
        tryShot(hit, realDirection);
    }

    private void tryShot(RaycastHit hit, Vector3 realDirection) {
        if (Input.GetButtonDown("Fire1")) {
            skillsButtonOne(hit, realDirection);
        } else if (Input.GetButtonDown("Fire2"))
        {
            skillsButtonTwo(hit, realDirection);
        }
    }

	private void skillsButtonOne(RaycastHit hit, Vector3 realDirection) {
        if (skill == HOOK) {
            if (auxGancho == null){
                anim.SetTrigger("Grab");
                source.PlayOneShot(soundShotHook, volSoundShotHook);
                auxGancho = Instantiate(hookPrefab, transform.position, Quaternion.LookRotation(realDirection)) as GameObject;
                auxGancho.GetComponent<PlayerHook>().player = gameObject;
            }else{
                anim.SetTrigger("Pull");
            }
        }
        else if (skill == PILLAR)
        {
            if (pillarPrefab.GetComponent<Pillar>().Mana < player.CurrentMana)
            {
                spawnPillar(hit, pillarPrefab, pillarPrefab.GetComponent<Pillar>().time);
                anim.SetTrigger("Trap");
            }
            else {
                source.PlayOneShot(soundErrorLowMana, volSoundErrorLowMana);
            }
        }
        else if (skill == TRAPSLOW)
        {

			if (SkillConfig.TrapSlow.manaCost < player.CurrentMana)
            {
                spawnTrap(hit, trapSlowPrefab, trapSlowPrefab.GetComponent<TrapSlow>().time);
				player.takeMana(SkillConfig.TrapSlow.manaCost);
                anim.SetTrigger("Trap");
            }
            else {
                source.PlayOneShot(soundErrorLowMana, volSoundErrorLowMana);
            }

        }
        else if (skill == TRAPDAMAGE){
			if (SkillConfig.TrapDamage.manaCost < player.CurrentMana){
                spawnTrap(hit, trapDamagePrefab, trapDamagePrefab.GetComponent<TrapDamage>().time);
				player.takeMana(SkillConfig.TrapDamage.manaCost);
                anim.SetTrigger("Trap");
            }else {
                source.PlayOneShot(soundErrorLowMana, volSoundErrorLowMana);
            }

        }
        else if (skill == BULLET){
            anim.SetTrigger("Attack");
            source.PlayOneShot(soundShotOne, volSoundShotOne);
            GameObject bulletAux = Instantiate(bulletPrefab, rightHand.position, Quaternion.LookRotation(realDirection)) as GameObject;
            CmdSpawnBullet(realDirection, bulletAux);
        }else if (skill == MARK){
            int costMana = 30;
            if (costMana < player.CurrentMana){
                SkillConfig.MarkOfTheStormConfig.Damage(body.position);

                Transform markOfTheStorm = transform.Find("MarkOfTheStorm");
                anim.SetTrigger("Shout");
                source.PlayOneShot(soundShoutDamageArea, volSoundShoutDamageArea);
                source.PlayOneShot(soundFlameDamageArea, volSoundFlameDamageArea);
                StartCoroutine(DelayShout(markOfTheStorm, costMana));

            }else {
                source.PlayOneShot(soundErrorLowMana, volSoundErrorLowMana);
            }
        }else if (skill == TRAPSTUN){
			if (SkillConfig.TrapStun.manaCost < player.CurrentMana){
                spawnTrap(hit, trapStunPrefab, trapStunPrefab.GetComponent<TrapStun>().time);
				player.takeMana(SkillConfig.TrapStun.manaCost);
                anim.SetTrigger("Trap");
            }else{
                source.PlayOneShot(soundErrorLowMana, volSoundErrorLowMana);
            }
        }
    }

    IEnumerator DelayShout(Transform markOfTheStorm, float costMana){
        yield return new WaitForSeconds(0.3f);
        if (markOfTheStorm){
            markOfTheStorm.gameObject.GetComponent<ParticleSystem>().Play();
        }else{
            print("mark of the storm not found on player!");
        }

        ParticleSystem explosion = GetComponentInChildren<ParticleSystem>();
        explosion.transform.position = body.position;
        //explosion.transform.rotation = new Quaternion (0, 0, 0, 0);
        explosion.Play();
        player.takeMana(costMana);
    }

    private void skillsButtonTwo(RaycastHit hit, Vector3 realDirection){
        if (skill == BULLET) {


			if (SkillConfig.StunBullet.manaCost < player.CurrentMana){
                anim.SetTrigger("Attack");
                source.PlayOneShot(soundShotTwo, volSoundShotTwo);
                GameObject bulletAux = Instantiate(bulletStunPrefab, rightHand.position, Quaternion.LookRotation(realDirection)) as GameObject;
                CmdSpawnBullet(realDirection, bulletAux);
				player.takeMana(SkillConfig.StunBullet.manaCost);
            }else{
                source.PlayOneShot(soundErrorLowMana, volSoundErrorLowMana);
            }
        }else if (skill == PILLAR){
			if(SkillConfig.ExplosivePillar.manaCost < player.CurrentMana){
				spawnPillar(hit, pillarExplosivePrefab, pillarExplosivePrefab.GetComponent<PillarExplosive>().time);
				player.takeMana(SkillConfig.ExplosivePillar.manaCost);
                anim.SetTrigger("Trap");
            }else{
                source.PlayOneShot(soundErrorLowMana, volSoundErrorLowMana);
            }
		}else if(skill == HOOK){
            if (auxGancho != null){
                anim.SetTrigger("Pull");
            }
        }
    }

	[Command]
    void CmdSpawnBullet(Vector3 realDirection, GameObject bulletAux)
    {
        bulletAux.GetComponent<Rigidbody>().velocity = realDirection * bulletSpeed;
        NetworkServer.Spawn(bulletAux);
    }
		

	private void spawnPillar(RaycastHit hit, GameObject pillar, int time)
	{
		GameObject hitObject = hit.collider.gameObject;
		if (hitObject != null)
		{
			CmdSpawnPillar(hitObject, pillar, time);
		}
	}

    [Command]
	private void CmdSpawnPillar(GameObject hitObject, GameObject pillar, int time) {
        TileGround tileGround = hitObject.GetComponentInParent<TileGround>();
        if (tileGround != null && tileGround.pillar == null) {
			tileGround.insertPillar(pillar, time);
        }
    }

	private void spawnTrap(RaycastHit hit, GameObject trap, int time){
		GameObject hitObject = hit.collider.gameObject;
		CmdSpawnTrap(hitObject, trap, time);
	}

	[Command]
	private void CmdSpawnTrap(GameObject hitObject, GameObject trap, int time){
		TileGround tileGround = hitObject.GetComponentInParent<TileGround>();
		if (tileGround != null && tileGround.trap == null)
		{
			tileGround.insertTrap(trap,time);
		}
	}
		

    private void calculateDirection(out RaycastHit hit, out Vector3 realDirection) {
		bool hasHit = Physics.Raycast(player.Cam.transform.position, player.Cam.transform.forward, out hit, 100);
        if (hit.collider == null) {
            hit.point = Camera.main.transform.position + Camera.main.transform.forward * 100f;
        } else {
			hit.distance = player.Cam.farClipPlane;
        }
        realDirection = hit.point - rightHand.position;
        lrMark.SetPosition(0, rightHand.position);
        lrMark.SetPosition(1, hit.point);
        mark.transform.position = hit.point;
    }

    private void selectSkill() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            skill = 1;
            updateButtonSKill();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            skill = 2;
            updateButtonSKill();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            skill = 3;
            updateButtonSKill();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            skill = 4;
            updateButtonSKill();
        }

		if (Input.GetKeyDown(KeyCode.Alpha5)) {
			skill = 5;
			updateButtonSKill();
		}

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            skill = 6;
            updateButtonSKill();
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            skill = 7;
            updateButtonSKill();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            skill++;
            if (skill > gameObjectsSkill.Length)
                skill = 1;
            updateButtonSKill();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            skill--;
            if (skill == 0)
                skill = gameObjectsSkill.Length;
            updateButtonSKill();
        }
    }

    private void updateButtonSKill() { 


        for (int i = 0; i < gameObjectsSkill.Length; i++) {
            gameObjectsSkill[i].gameObject.GetComponent<Button>().interactable = true;
        }
        gameObjectsSkill[skill - 1].gameObject.GetComponent<Button>().interactable = false;
    }
}
