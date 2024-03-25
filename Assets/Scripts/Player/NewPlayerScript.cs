using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;



public class NewPlayerScript : liveCharacter {

    //movimento
    [Header("Movimento")]
    public Transform precionandoCheck;
    public Animator anima;
    public bool andando, ladoE, ladoD;
    public float velocidade, lado;
    public float tempoSemMover;
    //pulo
    [Header("Pulo")]
    public Rigidbody2D personagem;
    public Transform groundCheck;
    public LayerMask chao;
    public bool Jump, grounded, caindo, DuploPulo;
    public float JumpForce;
    private float tempoPulando, tempoForaDoChao;
    public int numeroPulos;
    //tp
    [Header("TP")]
    public Transform paredeCheck;
    public GameObject painelTP;
    public Animator animadorTP;
    public bool possuiTp, paredeNoTp;
    private bool podeDarTp, deuTp, darTP/*, paredeNoTp*/;
    public float recargaTp, tpRange;
    private float tempopostp;
    public Light2D luz;

    //vida
    [Header("Vida")]
    public float vidaMax;
    public float ladoInimigo;
    public float tempoInvencivel;
    public float velocidadeMorte;
    public bool invencivel;
    public bool sofreuDano;
    public float knockBackForce;
    private bool dead, deadSpeed;
    //fogo grego
    [Header("Fogo grego")]
    public Animator animadorFogo;
    public GameObject fogo;
    public GameObject painelFogo;
    public bool possuiFogo;
    public bool fogoBool;
    public float tempoDeRecargaTiro;
    public float tempoMaxFogo;
    private bool TiroEmRecarga;
    private float tempoEmRecargaTiro/*, tempoCastingFogo*/;
    private GameObject[] pool = new GameObject[3];
    //lança
    [Header("Lança")]
    public bool lanca;
    public float tempoLanca;
    public Animator animadorLanca;
    //espada
    [Header("Espada")]
    public bool espada;
    public float tempoEspada;
    public Animator animadorEspada;
    //tempo
    [Header("Tempo")]
    public float tempo;
    //outros
    [Header("Outros")]
    public GameObject painelPru;
    public GameObject faseDetail;
    public static NewPlayerScript Player;
    public LayerMask enemyLayer;
    public float enemyNearRange;
    public GameObject playerUI;
    public bool voando;

    private delegate void updateDelegate();

    private updateDelegate updateFunctions;

    void Awake() {
        if (Player == null) {
            Player = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start() {
        verificaNulls();
        loadGameStats();
        GameObject.Find("Skills").SetActive(true);
        ladoD = true;
        ladoE = false;
        vida = vidaMax;
        tempoSemMover = 0;
        dead = false;
        voando = false;

        for (int i = 0; i < 3; i++) {
            pool[i] = Instantiate(fogo) as GameObject;
            pool[i].SetActive(false);
        }

        updateFunctions += andar;
        updateFunctions += verificaEspada;
        updateFunctions += verificaLanca;
        updateFunctions += pular;
        updateFunctions += instanciaFogo;
        updateFunctions += verificaTP;
        updateFunctions += verificaInvencivel;
        updateFunctions += verificaInimigosProximo;
        updateFunctions += voar;
    }

    // Update is called once per frame
    void Update() {
        /* andar();
        verificaEspada();
        verificaLanca();
        pular();
        instanciaFogo();
        verificaTP(); */

        updateFunctions();

        if (vida <= 0 && dead == false) {
            dead = true;
            deadSpeed = true;
            PlayerDeath.Instance.Kill();
            anima.SetTrigger("Morte");
            tempoSemMover = 6.0f;
        }

        if (deadSpeed) {
            personagem.velocity = new Vector3(velocidadeMorte * (lado * -1), personagem.velocity.y, 0);
        }

        //verificaInvencivel();

        tempo = Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.B)) {
            vida -= 10;
        }
    }

    public void DoFade() {
        Fade.Instance.FadeToGameOver();
    }

    public void LoadGameOver() {
        StartCoroutine(morreu());
    }

    public void StopDeathVelocity() {
        deadSpeed = false;
        personagem.velocity = new Vector3(0, personagem.velocity.y, 0);
    }

    IEnumerator morreu() {
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Additive);
        while (!asyncLoad.isDone) {
            yield return null;
        }
        SceneManager.MoveGameObjectToScene(faseDetail, SceneManager.GetSceneByName("GameOver"));
        SceneManager.UnloadSceneAsync(currentScene);
    }

    private void FixedUpdate() {
        ground();
        painelFogo.SetActive(possuiFogo);
        painelTP.SetActive(possuiTp);
        painelPru.SetActive(false);
        //Debug.Log(DifficultyController.instance.getDifficulty());
    }

    public void verificaInvencivel() {
        if (tempoSemMover >= 0) {
            tempoSemMover -= tempo;
        }
        if (invencivel) {
            tempoInvencivel -= tempo;
        }
        if (tempoInvencivel >= 0) {
            invencivel = false;
        }
    }

    public override void GetDamage(float Damage) {
        if (!invencivel) {
            var dificultScaler = DifficultyController.instance.getDifficultyDamageScaler();
            vida -= (Damage * dificultScaler);
        }
    }

    public void verificaInimigosProximo() {
        bool enemyNear = Physics2D.OverlapCircle(groundCheck.position, enemyNearRange, enemyLayer);
        if ((enemyNear || (this.vida / this.vidaMax < 0.3f)) && !playerUI.activeInHierarchy) {
            playerUI.SetActive(true);
        }else if(!(enemyNear || (this.vida / this.vidaMax < 0.3f)) && playerUI.activeInHierarchy) {
            playerUI.SetActive(false);
        }
    }

    public void instanciaFogo() {
        painelFogo.SetActive(possuiFogo);
        if (Input.GetButtonDown("Fire3") && TiroEmRecarga == false && possuiFogo && grounded) {
            fogoBool = true;
            // tempoCastingFogo = 0;

            TiroEmRecarga = true;
            tempoEmRecargaTiro = 0;

            anima.SetTrigger("foguito");
            tempoSemMover = 0.9f;
        }
        if (TiroEmRecarga) {
            if (tempoEmRecargaTiro >= tempoDeRecargaTiro) {
                TiroEmRecarga = false;
            } else {
                tempoEmRecargaTiro += tempo;
            }
        }



        /*if (fogoBool) {
            tempoCastingFogo += tempo;
            if (tempoCastingFogo >= tempoMaxFogo && fogoBool) {
                fogoBool = false;  
            }
        }*/

        animadorFogo.SetBool("fogoRecarga", TiroEmRecarga);
        //anima.SetBool("Fire", fogoBool);
    }

    public void voar() {
        if (Input.GetKeyDown(KeyCode.V)) {
            voando = !voando;
            anima.SetBool("voando", voando);
            if (voando) {
                personagem.gravityScale = 0f;
            } else {
                personagem.gravityScale = 1.5f;
            }
        }

        if (Input.GetAxisRaw("Vertical") != 0 && voando) {
            lado = Input.GetAxisRaw("Vertical");
            if (!precionandoParede()) {
                transform.position += new Vector3(0, lado, 0) * tempo * velocidade;
            }
        }
    }

    public void CriaFogo() {
        //GameObject tempPrefab = Instantiate(fogo) as GameObject;
        GameObject tempPrefab = null;
        for (int i = 0; i < 3; i++) {
            if (pool[i].activeSelf == false) {
                tempPrefab = pool[i];
                tempPrefab.SetActive(true);
                tempPrefab.GetComponent<fogo_grego>().Start();

                //tempPrefab.GetComponent<fogo_grego>().tempo = 0;
                //tempPrefab.GetComponent<fogo_grego>().lado = this.lado;
                //if(this.lado == -1) {
                //    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                //}
                break;
            }
        }
        tempPrefab.transform.position = new Vector3(transform.position.x, transform.position.y, tempPrefab.transform.position.z);
        fogoBool = false;
    }

    public void loadGameStats() {
        try {
            PlayerInfo info = SaveLoad.LoadPlayer();

            if (info != null) {
                possuiFogo = info.possuiFogo;
                //possuiAguia = info.possuiAguia;
                possuiTp = info.possuiTp;
                Debug.Log("Carreguei!");
            }
        } catch (Exception e) {
            Debug.Log(e.ToString());
        }
    }

    private void andar() {
        if (Input.GetAxisRaw("Horizontal") != 0 && deuTp == false && podeAndar() && espada == false) {
            lado = Input.GetAxisRaw("Horizontal");
            andando = true;
            if (!precionandoParede()) {
                transform.position += new Vector3(lado, 0, 0) * tempo * velocidade;
            } else {
                andando = false;
            }
            if (ladoE && lado > 0) {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                ladoE = false;
                ladoD = true;
            }
            if (ladoD && lado < 0) {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                ladoE = true;
                ladoD = false;
            }
        } else {
            andando = false;
        }
        anima.SetBool("andar", andando);
    }

    private void pular() {
        if (grounded) {
            numeroPulos = 0;
            tempoForaDoChao = 0;
            caindo = false;
        } else {
            tempoForaDoChao += tempo;
            caindo = true;
        }
        if (tempoForaDoChao >= 3f) {
            Jump = false;
            DuploPulo = false;
        }

        if (Input.GetButtonDown("Jump") && numeroPulos < 1 && !fogoBool) {
            Jump = true;
            numeroPulos++;
            tempoPulando = 0;
            personagem.velocity = Vector2.up * JumpForce;
            if (numeroPulos > 0) {
                DuploPulo = true;
            }
        } else {
            if (Jump) {
                if (grounded && tempoPulando >= 0.1) {
                    Jump = false;
                } else if (tempoPulando < 0.1) {
                    tempoPulando += tempo;
                }
            }
        }
        if (grounded == true && DuploPulo == true) {
            DuploPulo = false;
        }

        anima.SetBool("caindo", caindo);
        anima.SetBool("pulando", Jump);
        anima.SetBool("DuploPulo", DuploPulo);
    }

    private bool precionandoParede() {
        if (Physics2D.OverlapCircle(precionandoCheck.position, 0.2f, chao)) {
            /*while (Physics2D.OverlapCircle(precionandoCheck.position, 0.1f, chao))
            {
                transform.position -= new Vector3(lado, 0, 0) * 0.01f * tempo;
            }*/
            return true;
        }
        return false;
    }

    private bool podeAndar() {
        if (lanca || espada || tempoSemMover > 0) {
            return false;
        }

        return true;
    }

    private void ground() {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, chao);
    }

    private bool podeSeMover() {
        if (!grounded) {
            return false;
        }
        return true;
    }

    private void verificaEspada() {
        /*if (Input.GetButtonDown("espada") && !espada)
        {
            
            soundControllerScript.PlaySound("espada");
            
            espada = true;
            tempoEspada = 0;
            
        }
        if (espada)
        {
            tempoEspada += tempo;
            if(tempoEspada >= 0.25f)
            {
                espada = false;
            }
        }*/
        //animadorEspada.SetBool("espadaRecarga", espada);
    }

    public float GetVida() {
        return vida;
    }

    public void verificaLanca() {
        /* if (Input.GetButtonDown("lanca") && Jump == false)
         {
             lanca = true;
             tempoLanca = 0;
             tempoSemMover = 0.5f;
         }
         if (lanca)
         {
             tempoLanca += tempo;
         }
         if (tempoLanca >= 0.5f)
         {
             lanca = false;
         }*/
        //animadorLanca.SetBool("lancaRecarga", lanca);
    }

    public bool paredeNaFrenteTP() {
        paredeCheck.transform.position = new Vector3(transform.position.x + (tpRange * lado), paredeCheck.transform.position.y, paredeCheck.transform.position.z);
        if (Physics2D.OverlapCircle(paredeCheck.position, 0.2f, chao)) {
            return true;
        }
        return false;
    }

    public void verificaTP() {
        paredeNoTp = paredeNaFrenteTP();
        if (Input.GetButtonDown("tp") && podeDarTp && paredeNoTp == false && grounded == true && possuiTp) {
            deuTp = true;
            tempopostp = 0;
            podeDarTp = false;
            invencivel = true;
            tempoInvencivel = 1.5f;
            anima.SetTrigger("TP");
            StartCoroutine("TpFade");
            //darTP = true;
        }
        /*if (darTP && tempopostp >= 0.5f)
        {
            transform.position = new Vector3(transform.position.x + (lado * tpRange), transform.position.y, transform.position.z);
            darTP = false;
        }*/
        if (deuTp && tempopostp >= 1f) {
            deuTp = false;
        }
        if (podeDarTp == false && tempopostp >= recargaTp) {
            podeDarTp = true;
        } else {
            tempopostp += tempo;
        }

        animadorTP.SetBool("Tp_recarga", !podeDarTp);
    }

    IEnumerator TpFade() {
        float time = 0;
        while (true) {
            if (time <= 0.5f) {
                luz.intensity -= 0.09f;
            } else {
                luz.intensity += 0.09f;
            }

            yield return new WaitForSeconds(.1f);
            time += 0.1f;
            if (time >= 1f) {
                luz.intensity = 0.5f;
                break;
            }
        }
    }

    public void DoTP() {
        transform.position = new Vector3(transform.position.x + (lado * tpRange), transform.position.y, transform.position.z);
    }

    public void Damage(AttackDetails atq) {
        if (!invencivel) {
            vida -= atq.damageAmount;
            invencivel = true;
            tempoInvencivel = 1f;
            tempoSemMover = 1f;
            if (atq.position.x > transform.position.x) {
                personagem.velocity = Vector2.left * knockBackForce;
            } else {
                personagem.velocity = Vector2.right * knockBackForce;
            }
        }

    }

    public void verificaNulls() {
        if (painelFogo == null) {
            painelFogo = GameObject.Find("Fogo");
        }
        if (painelTP == null) {
            painelTP = GameObject.Find("Tp");
        }
        if (painelPru == null) {
            painelPru = GameObject.Find("Ave");
        }
        if (precionandoCheck == null) {
            precionandoCheck = GameObject.Find("Precionando").GetComponent<Transform>();
        }
        /*if(animadorLanca == null)
        {
            animadorLanca = GameObject.Find("Ave").GetComponent<Animator>();
        }*/
        /*if (animadorEspada == null)
        {
            animadorEspada = GameObject.Find("Espada").GetComponent<Animator>();
        }*/
        if (animadorFogo == null) {
            animadorFogo = GameObject.Find("Fogo").GetComponent<Animator>();
        }
        if (animadorTP == null) {
            animadorTP = GameObject.Find("Tp").GetComponent<Animator>();
        }
        if (groundCheck == null) {
            groundCheck = GameObject.Find("check").GetComponent<Transform>();
        }
        if (anima == null) {
            anima = GameObject.Find("personagem").GetComponent<Animator>();
        }
        if (personagem == null) {
            personagem = GameObject.Find("personagem").GetComponent<Rigidbody2D>();
        }
        if (paredeCheck == null) {
            paredeCheck = GameObject.Find("checkParede").GetComponent<Transform>();
        }
        if (faseDetail == null) {
            faseDetail = GameObject.Find("faseDetail");
        }
        if (luz == null) {
            luz = GameObject.Find("luzPersonagem").GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        }
        //personagem, paredecheck
    }

    private void OnDrawGizmos() { 
        Gizmos.DrawWireSphere(this.gameObject.transform.position, enemyNearRange);

        if (paredeCheck == null)
            return;
        Gizmos.DrawWireSphere(paredeCheck.position, 0.2f);
    }
}
