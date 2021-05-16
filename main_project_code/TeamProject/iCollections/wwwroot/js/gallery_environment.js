import * as THREE from './three.module.js';
import { PointerLockControls } from './PointerLockControls.js';
import { GatherPhotoData, LoadImagesToScene, LoadDemoImagesToScene } from './environment_functions.js';


$(document).ready(function MakeGallery() {
    console.log("page loaded the gallery environment.js");

    //Gets the photo data written to the tr's in the DOM
    var photoData = [];
    photoData = GatherPhotoData(photoData);

    let photoDemoData = [
        { "srcData": './images/card_pics/IMG-0782.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0783.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0784.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0785.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0786.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0787.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0788.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0789.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0790.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0791.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0792.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0793.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0797.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0798.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0799.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0800.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0801.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0802.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0803.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0804.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0805.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0806.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0807.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0808.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0809.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0810.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0811.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0812.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0813.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0814.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0815.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0816.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0817.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0818.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0819.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0820.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0821.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0822.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0823.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0824.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0825.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0826.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0827.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0828.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0829.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
    ];

    /*let photoDemoData = [
        { "srcData": './images/card_pics/IMG-0782.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0783.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0784.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0785.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0786.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0787.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0788.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0789.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0790.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0791.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
        { "srcData": './images/card_pics/IMG-0792.JPG', "srcTitle": 'Future Title', "srcRank": 1, "srcDescription": "Future Description" },
    ];*/







    if (photoData.length === 0) {
        console.log("Photo Data is NUll")
    }



    let camera, scene, renderer, controls, container;
    let floorMat, wallMat;

    const objects = [];

    let raycaster;

    let moveForward = false;
    let moveBackward = false;
    let moveLeft = false;
    let moveRight = false;
    let canJump = false;

    let prevTime = performance.now();
    const velocity = new THREE.Vector3();
    const direction = new THREE.Vector3();

    init();
    animate();

    function init() {

        //camera
        camera = new THREE.PerspectiveCamera(55, window.innerWidth / window.innerHeight, 1, 20000);
        camera.position.y = 300;

        //basic scene attributes
        scene = new THREE.Scene();
        scene.background = new THREE.Color(0xcce0ff);
        //scene.fog = new THREE.Fog(0xffffff, 500, 10000);

        //General light source
        const light = new THREE.HemisphereLight(0xeeeeff, 0x777788, 0.75);
        light.position.set(0.5, 1, 0.75);
        scene.add(light);

        // Skybox

        var nightGeometry = new THREE.BoxGeometry(20000, 20000, 20000);
        var cubeMaterials = [
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("images/night_skybox/nightsky_ft.png"), side: THREE.DoubleSide }), //front side
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load('images/night_skybox/nightsky_bk.png'), side: THREE.DoubleSide }), //back side
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load('images/night_skybox/nightsky_up.png'), side: THREE.DoubleSide }), //up side
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load('images/night_skybox/nightsky_dn.png'), side: THREE.DoubleSide }), //down side
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load('images/night_skybox/nightsky_rt.png'), side: THREE.DoubleSide }), //right side
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load('images/night_skybox/nightsky_lf.png'), side: THREE.DoubleSide }) //left side
        ];

        var cubeMaterial = new THREE.MeshFaceMaterial(cubeMaterials);
        var cube = new THREE.Mesh(nightGeometry, cubeMaterial);
        cube.position.y = 2000;
        scene.add(cube);

        // lighting
        //var ambientLight = new THREE.AmbientLight(0xFFFFFF, 0.3);
       // scene.add(ambientLight);







        // Creates the event liseners for the pointerlock controls and key / mouse functions
        controls = new PointerLockControls(camera, document.body);

        const blocker = document.getElementById('blocker');
        const instructions = document.getElementById('instructions');

        instructions.addEventListener('click', function () {

            controls.lock();

        });

        controls.addEventListener('lock', function () {

            instructions.style.display = 'none';
            blocker.style.display = 'none';

        });

        controls.addEventListener('unlock', function () {

            blocker.style.display = 'block';
            instructions.style.display = '';

        });

        scene.add(controls.getObject());

        const onKeyDown = function (event) {

            switch (event.code) {

                case 'ArrowUp':
                case 'KeyW':
                    moveForward = true;
                    break;

                case 'ArrowLeft':
                case 'KeyA':
                    moveLeft = true;
                    break;

                case 'ArrowDown':
                case 'KeyS':
                    moveBackward = true;
                    break;

                case 'ArrowRight':
                case 'KeyD':
                    moveRight = true;
                    break;

                case 'Space':
                    if (canJump === true) velocity.y += 350;
                    canJump = false;
                    break;

            }

        };

        const onKeyUp = function (event) {

            switch (event.code) {

                case 'ArrowUp':
                case 'KeyW':
                    moveForward = false;
                    break;

                case 'ArrowLeft':
                case 'KeyA':
                    moveLeft = false;
                    break;

                case 'ArrowDown':
                case 'KeyS':
                    moveBackward = false;
                    break;

                case 'ArrowRight':
                case 'KeyD':
                    moveRight = false;
                    break;

            }

        };

        document.addEventListener('keydown', onKeyDown);
        document.addEventListener('keyup', onKeyUp);

        raycaster = new THREE.Raycaster(new THREE.Vector3(), new THREE.Vector3(0, - 1, 0), 0, 10);



        // Hardwood floor replacement 

        let floorGeometry = new THREE.PlaneGeometry(9000, 9000);
        floorGeometry.rotateX(- Math.PI / 2);

        floorMat = new THREE.MeshStandardMaterial({
            roughness: 0.8,
            color: 0xffffff,
            metalness: 0.2,
            bumpScale: 0.0005
        });
        const textureLoader = new THREE.TextureLoader();
        textureLoader.load("images/textures/hardwood2_diffuse.jpg", function (map) {

            map.wrapS = THREE.RepeatWrapping;
            map.wrapT = THREE.RepeatWrapping;
            map.anisotropy = 4;
            map.repeat.set(10, 24);
            map.encoding = THREE.sRGBEncoding;
            floorMat.map = map;
            floorMat.needsUpdate = true;

        });
        textureLoader.load("images/textures/hardwood2_bump.jpg", function (map) {

            map.wrapS = THREE.RepeatWrapping;
            map.wrapT = THREE.RepeatWrapping;
            map.anisotropy = 4;
            map.repeat.set(10, 24);
            floorMat.bumpMap = map;
            floorMat.needsUpdate = true;

        });
        textureLoader.load("images/textures/hardwood2_roughness.jpg", function (map) {

            map.wrapS = THREE.RepeatWrapping;
            map.wrapT = THREE.RepeatWrapping;
            map.anisotropy = 4;
            map.repeat.set(10, 24);
            floorMat.roughnessMap = map;
            floorMat.needsUpdate = true;

        });

        const floor = new THREE.Mesh(floorGeometry, floorMat);

        scene.add(floor);



        // Testing Walls group creation


        wallMat = new THREE.MeshStandardMaterial({
            roughness: 0.7,
            color: 0xffffff,
            bumpScale: 0.002,
            metalness: 0.2
        });
        textureLoader.load("images/textures/brick_diffuse.jpg", function (map) {

            map.wrapS = THREE.RepeatWrapping;
            map.wrapT = THREE.RepeatWrapping;
            map.anisotropy = 4;
            map.repeat.set(7, 4);
            map.encoding = THREE.sRGBEncoding;
            wallMat.map = map;
            wallMat.needsUpdate = true;

        });
        textureLoader.load("images/textures/brick_bump.jpg", function (map) {

            map.wrapS = THREE.RepeatWrapping;
            map.wrapT = THREE.RepeatWrapping;
            map.anisotropy = 4;
            map.repeat.set(7, 4);
            wallMat.bumpMap = map;
            wallMat.needsUpdate = true;

        });

        //const boxGeometry = new THREE.BoxGeometry(10000, 2000, 500);
        //const boxMesh = new THREE.Mesh(boxGeometry, wallMat);
       // boxMesh.position.set(10000, 2000, -500);
       // scene.add(boxMesh);

        let wallGroup = new THREE.Group();
        scene.add(wallGroup);

        let wall1 = new THREE.Mesh(new THREE.BoxGeometry(9000, 2000, 1), wallMat);
        let wall2 = new THREE.Mesh(new THREE.BoxGeometry(9000, 2000, 1), wallMat);
        let wall3 = new THREE.Mesh(new THREE.BoxGeometry(9000, 2000, 1), wallMat);
        let wall4 = new THREE.Mesh(new THREE.BoxGeometry(9000, 2000, 1), wallMat);

        wallGroup.add(wall1, wall2, wall3, wall4);
        wallGroup.position.y = 3;

        wall1.position.z = -4500;
        wall2.position.x = -4500;
        wall2.rotation.y = Math.PI / 2;
        wall3.position.x = 4500;
        wall3.rotation.y = -Math.PI / 2;
        wall4.position.z = 4500;
        wall4.rotation.y = Math.PI;

        for (var i = 0; i < wallGroup.children.length; i++) {
            wallGroup.children[i].BBox = new THREE.Box3();
            wallGroup.children[i].BBox.setFromObject(wallGroup.children[i]);
        }


        //Brick Wall Replacement

        /*let wallGeometry = new THREE.BoxGeometry(10000, 2000, 1);
        //wallGeometry.position.z = -5000;

        wallMat = new THREE.MeshStandardMaterial({
            roughness: 0.7,
            color: 0xffffff,
            bumpScale: 0.002,
            metalness: 0.2
        });
        textureLoader.load("images/textures/brick_diffuse.jpg", function (map) {

            map.wrapS = THREE.RepeatWrapping;
            map.wrapT = THREE.RepeatWrapping;
            map.anisotropy = 4;
            map.repeat.set(1, 1);
            map.encoding = THREE.sRGBEncoding;
            wallMat.map = map;
            wallMat.needsUpdate = true;

        });
        textureLoader.load("images/textures/brick_bump.jpg", function (map) {

            map.wrapS = THREE.RepeatWrapping;
            map.wrapT = THREE.RepeatWrapping;
            map.anisotropy = 4;
            map.repeat.set(1, 1);
            wallMat.bumpMap = map;
            wallMat.needsUpdate = true;

        });

        //const walldemo = new THREE.Mesh(wallGeometry, wallMat);
        //scene.add(walldemo);
        const boxMesh = new THREE.Mesh(wallGeometry, wallMat);
        boxMesh.position.set(- 0.5, 0.25, - 1);
        boxMesh.castShadow = true;
        scene.add(boxMesh);*/

        



        //Photo locations 
        let positionCordinateData = [
            //Inner Circle
            { "xAxis": 0, "yAxis": 250, "zAxis": -1300, "yRotation": null },
            { "xAxis": 1300, "yAxis": 250, "zAxis": 0, "yRotation": -1.57 },
            { "xAxis": 0, "yAxis": 250, "zAxis": 1300, "yRotation": Math.PI },
            { "xAxis": -1300, "yAxis": 250, "zAxis": 0, "yRotation": 1.57 },
            { "xAxis": 1300, "yAxis": 250, "zAxis": -1300, "yRotation": -0.78 },
            { "xAxis": -1300, "yAxis": 250, "zAxis": -1300, "yRotation": 0.78 },
            { "xAxis": 1300, "yAxis": 250, "zAxis": 1300, "yRotation": 3.92 },
            { "xAxis": -1300, "yAxis": 250, "zAxis": 1300, "yRotation": -3.92 },

            //Second Layer
            { "xAxis": 700, "yAxis": 250, "zAxis": -2300, "yRotation": null },
            { "xAxis": -700, "yAxis": 250, "zAxis": -2300, "yRotation": null },
            { "xAxis": 2300, "yAxis": 250, "zAxis": 700, "yRotation": -1.57 },
            { "xAxis": 2300, "yAxis": 250, "zAxis": -700, "yRotation": -1.57 },
            { "xAxis": 700, "yAxis": 250, "zAxis": 2300, "yRotation": Math.PI },
            { "xAxis": -700, "yAxis": 250, "zAxis": 2300, "yRotation": Math.PI },
            { "xAxis": -2300, "yAxis": 250, "zAxis": 700, "yRotation": 1.57 },
            { "xAxis": -2300, "yAxis": 250, "zAxis": -700, "yRotation": 1.57 },
            { "xAxis": 2300, "yAxis": 250, "zAxis": -2300, "yRotation": -0.78 },
            { "xAxis": -2300, "yAxis": 250, "zAxis": -2300, "yRotation": 0.78 },
            { "xAxis": 2300, "yAxis": 250, "zAxis": 2300, "yRotation": 3.92 },
            { "xAxis": -2300, "yAxis": 250, "zAxis": 2300, "yRotation": -3.92 },


            //Third Layer
            { "xAxis": 0, "yAxis": 250, "zAxis": -3300, "yRotation": null },
            { "xAxis": 1200, "yAxis": 250, "zAxis": -3300, "yRotation": null },
            { "xAxis": -1200, "yAxis": 250, "zAxis": -3300, "yRotation": null },
            { "xAxis": 2200, "yAxis": 250, "zAxis": -3300, "yRotation": null },
            { "xAxis": -2200, "yAxis": 250, "zAxis": -3300, "yRotation": null },

            { "xAxis": 3300, "yAxis": 250, "zAxis": 0, "yRotation": -1.57 },
            { "xAxis": 3300, "yAxis": 250, "zAxis": 1200, "yRotation": -1.57 },
            { "xAxis": 3300, "yAxis": 250, "zAxis": -1200, "yRotation": -1.57 },
            { "xAxis": 3300, "yAxis": 250, "zAxis": 2200, "yRotation": -1.57 },
            { "xAxis": 3300, "yAxis": 250, "zAxis": -2200, "yRotation": -1.57 },

            { "xAxis": 0, "yAxis": 250, "zAxis": 3300, "yRotation": Math.PI },
            { "xAxis": 1200, "yAxis": 250, "zAxis": 3300, "yRotation": Math.PI },
            { "xAxis": -1200, "yAxis": 250, "zAxis": 3300, "yRotation": Math.PI },
            { "xAxis": 2200, "yAxis": 250, "zAxis": 3300, "yRotation": Math.PI },
            { "xAxis": -2200, "yAxis": 250, "zAxis": 3300, "yRotation": Math.PI },

            { "xAxis": -3300, "yAxis": 250, "zAxis": 0, "yRotation": 1.57 },
            { "xAxis": -3300, "yAxis": 250, "zAxis": 1200, "yRotation": 1.57 },
            { "xAxis": -3300, "yAxis": 250, "zAxis": -1200, "yRotation": 1.57 },
            { "xAxis": -3300, "yAxis": 250, "zAxis": 2200, "yRotation": 1.57 },
            { "xAxis": -3300, "yAxis": 250, "zAxis": -2200, "yRotation": 1.57 },


            { "xAxis": 3300, "yAxis": 250, "zAxis": -3300, "yRotation": -0.78 },
            { "xAxis": -3300, "yAxis": 250, "zAxis": -3300, "yRotation": 0.78 },
            { "xAxis": 3300, "yAxis": 250, "zAxis": 3300, "yRotation": 3.92 },
            { "xAxis": -3300, "yAxis": 250, "zAxis": 3300, "yRotation": -3.92 },

            //Fourth Layer
            { "xAxis": 0, "yAxis": 250, "zAxis": -4300, "yRotation": null },
            { "xAxis": 4300, "yAxis": 250, "zAxis": 0, "yRotation": -1.57 },
            { "xAxis": 0, "yAxis": 250, "zAxis": 4300, "yRotation": Math.PI },
            { "xAxis": -4300, "yAxis": 250, "zAxis": 0, "yRotation": 1.57 },
            { "xAxis": 4300, "yAxis": 250, "zAxis": -4300, "yRotation": -0.78 },
            { "xAxis": -4300, "yAxis": 250, "zAxis": -4300, "yRotation": 0.78 },
            { "xAxis": 4300, "yAxis": 250, "zAxis": 4300, "yRotation": 3.92 },
            { "xAxis": -4300, "yAxis": 250, "zAxis": 4300, "yRotation": -3.92 },

        ];



        LoadImagesToScene(scene, photoData, positionCordinateData);

        if (photoData.length === 0) {
            LoadDemoImagesToScene(scene, photoDemoData, positionCordinateData);
        }

        // old hard coded image vectors ---------------------
        /*puzzel_29 = uploadImage('images/puzzel_pics/image_123923953.JPG');
        puzzel_29.position.set(2000, 250, 3500);
        puzzel_29.rotation.y = Math.PI;
        scene.add(puzzel_29);*/
        

        container = document.getElementById('gallery_environment');

        renderer = new THREE.WebGLRenderer({ antialias: true });
        renderer.setPixelRatio(window.devicePixelRatio);
        //renderer.setSize(window.innerWidth, window.innerHeight);
        renderer.setSize(document.getElementById('gallery_environment').clientWidth, parent.innerHeight);

        container.appendChild(renderer.domElement);

        window.addEventListener('resize', onWindowResize);

    }

    function onWindowResize() {

        camera.aspect = window.innerWidth / window.innerHeight;
        camera.updateProjectionMatrix();

        //renderer.setSize(window.innerWidth, window.innerHeight);
        renderer.setSize(document.getElementById('gallery_environment').clientWidth, parent.innerHeight);
    }

    function animate() {

        requestAnimationFrame(animate);

        const time = performance.now();

        if (controls.isLocked === true) {

            raycaster.ray.origin.copy(controls.getObject().position);
            raycaster.ray.origin.y -= 300;

            const intersections = raycaster.intersectObjects(objects);

            const onObject = intersections.length > 0;

            const delta = (time - prevTime) / 1000;

            velocity.x -= velocity.x * 10.0 * delta;
            velocity.z -= velocity.z * 10.0 * delta;

            velocity.y -= 9.8 * 100.0 * delta; // 100.0 = mass

            direction.z = Number(moveForward) - Number(moveBackward);
            direction.x = Number(moveRight) - Number(moveLeft);
            direction.normalize(); // this ensures consistent movements in all directions

            if (moveForward || moveBackward) velocity.z -= direction.z * 8000.0 * delta;
            if (moveLeft || moveRight) velocity.x -= direction.x * 8000.0 * delta;

            if (onObject === true) {

                velocity.y = Math.max(0, velocity.y);
                canJump = true;

            }

            controls.moveRight(- velocity.x * delta);
            controls.moveForward(- velocity.z * delta);

            controls.getObject().position.y += (velocity.y * delta); // new behavior
            controls.getObject().position.y = 300; // new behavior

            if (controls.getObject().position.y < 10) {

                velocity.y = 0;
                controls.getObject().position.y = 300;

                canJump = true;

            }

        }

        prevTime = time;

        renderer.render(scene, camera);

    }

});