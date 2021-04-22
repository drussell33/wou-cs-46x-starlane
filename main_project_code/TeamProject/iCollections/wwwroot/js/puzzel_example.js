import * as THREE from './three.module.js';
import { PointerLockControls } from './PointerLockControls.js';


$(document).ready(function MakeGallery() {
    console.log("page loaded the gallery environment.js");



    var photoData = [];


    //let testPhotoSrc;
    //let counter = 1;
    $("tr").each(function () {
        //let name = "photo" + counter.toString();
        photoData.push({
            srcData: $(this).attr("data-photodata"),
            srcTitle: $(this).attr("data-title"),
            srcRank: $(this).attr("data-rank"),
            srcDescription: $(this).attr("data-description")

        });
        console.log($(this).attr("data-title"));
        console.log($(this).attr("data-rank"));
        console.log($(this).attr("data-description"));
        //testPhotoSrc = $(this).attr("data-photodata");
        //counter++;
    });

    //console.log(testPhotoSrc);










    let camera, scene, renderer, controls, container;
    let floorMat;
    let puzzel_1, puzzel_2, puzzel_3, puzzel_4, puzzel_5, puzzel_6, puzzel_7, puzzel_8, puzzel_9, puzzel_10;
    let puzzel_11, puzzel_12, puzzel_13, puzzel_14, puzzel_15, puzzel_16, puzzel_17, puzzel_18, puzzel_19, puzzel_20;
    let puzzel_21, puzzel_22, puzzel_23, puzzel_24, puzzel_25, puzzel_26, puzzel_27, puzzel_28, puzzel_29, puzzel_30;


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
    const vertex = new THREE.Vector3();
    const color = new THREE.Color();

    init();
    animate();

    function init() {

        camera = new THREE.PerspectiveCamera(55, window.innerWidth / window.innerHeight, 1, 20000);
        camera.position.y = 300;

        scene = new THREE.Scene();
        scene.background = new THREE.Color(0xcce0ff);
        scene.fog = new THREE.Fog(0xffffff, 500, 10000);

        const light = new THREE.HemisphereLight(0xeeeeff, 0x777788, 0.75);
        light.position.set(0.5, 1, 0.75);
        scene.add(light);

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


        // Testing Walls
        let wallGroup = new THREE.Group();
        scene.add(wallGroup);

        let wall1 = new THREE.Mesh(new THREE.BoxGeometry(10000, 2000, 0.001), new THREE.MeshLambertMaterial({ color: 0xffffff }));
        let wall2 = new THREE.Mesh(new THREE.BoxGeometry(10000, 2000, 0.001), new THREE.MeshLambertMaterial({ color: 0xffffff }));
        let wall3 = new THREE.Mesh(new THREE.BoxGeometry(10000, 2000, 0.001), new THREE.MeshLambertMaterial({ color: 0xffffff }));
        let wall4 = new THREE.Mesh(new THREE.BoxGeometry(10000, 2000, 0.001), new THREE.MeshLambertMaterial({ color: 0xffffff }));

        wallGroup.add(wall1, wall2, wall3, wall4);
        wallGroup.position.y = 3;

        wall1.position.z = -5000;
        wall2.position.x = -5000;
        wall2.rotation.y = Math.PI / 2;
        wall3.position.x = 5000;
        wall3.rotation.y = -Math.PI / 2;
        wall4.position.z = 5000;
        wall4.rotation.y = Math.PI;

        for (var i = 0; i < wallGroup.children.length; i++) {
            wallGroup.children[i].BBox = new THREE.Box3();
            wallGroup.children[i].BBox.setFromObject(wallGroup.children[i]);
        }

        // Testing Walls end








        // floor

        let floorGeometry = new THREE.PlaneGeometry(10000, 10000);
        floorGeometry.rotateX(- Math.PI / 2);

        // vertex displacement

        let position = floorGeometry.attributes.position;

        for (let i = 0, l = position.count; i < l; i++) {

            vertex.fromBufferAttribute(position, i);

            vertex.x += Math.random() * 20 - 10;
            vertex.y += Math.random() * 2;
            vertex.z += Math.random() * 20 - 10;

            position.setXYZ(i, vertex.x, vertex.y, vertex.z);

        }

        floorGeometry = floorGeometry.toNonIndexed(); // ensure each face has unique vertices

        position = floorGeometry.attributes.position;
        const colorsFloor = [];

        for (let i = 0, l = position.count; i < l; i++) {

            color.setHSL(Math.random() * 0.3 + 0.5, 0.75, Math.random() * 0.25 + 0.75);
            colorsFloor.push(color.r, color.g, color.b);

        }

        floorGeometry.setAttribute('color', new THREE.Float32BufferAttribute(colorsFloor, 3));

        const floorMaterial = new THREE.MeshBasicMaterial({ vertexColors: true });

        //hARDWOOD replacement start
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
        //hARDWOOD replacement start
        const floor = new THREE.Mesh(floorGeometry, floorMat);

        //const floor = new THREE.Mesh( floorGeometry, floorMaterial );
        scene.add(floor);

        // objects

        // const boxGeometry = new THREE.BoxGeometry( 20, 20, 20 ).toNonIndexed();

        // position = boxGeometry.attributes.position;
        // const colorsBox = [];

        // for ( let i = 0, l = position.count; i < l; i ++ ) {

        // 	color.setHSL( Math.random() * 0.3 + 0.5, 0.75, Math.random() * 0.25 + 0.75 );
        // 	colorsBox.push( color.r, color.g, color.b );

        // }

        // boxGeometry.setAttribute( 'color', new THREE.Float32BufferAttribute( colorsBox, 3 ) );

        // for ( let i = 0; i < 500; i ++ ) {

        // 	const boxMaterial = new THREE.MeshPhongMaterial( { specular: 0xffffff, flatShading: true, vertexColors: true } );
        // 	boxMaterial.color.setHSL( Math.random() * 0.2 + 0.5, 0.75, Math.random() * 0.25 + 0.75 );

        // 	const box = new THREE.Mesh( boxGeometry, boxMaterial );
        // 	box.position.x = Math.floor( Math.random() * 20 - 10 ) * 20;
        // 	box.position.y = Math.floor( Math.random() * 20 ) * 20 + 10;
        // 	box.position.z = Math.floor( Math.random() * 20 - 10 ) * 20;

        // 	scene.add( box );
        // 	objects.push( box );

        // }


        //Custom Upload Photo
        function uploadImage(collectionPhoto) {
            // create a canvas element
            var canvas = document.createElement('canvas');
            canvas.width = 500;
            canvas.height = 500;
            var context = canvas.getContext('2d');
            // canvas contents will be used for a texture
            var texture = new THREE.Texture(canvas);

            // load an image
            var imageObj = new Image();
            let newSrc = "data:image/png;base64," + collectionPhoto;
            imageObj.src = newSrc;
            // after the image is loaded, this function executes
            imageObj.onload = function () {
                context.drawImage(imageObj, 0, 0, imageObj.width, imageObj.height, 0, 0, 500, 500);
                if (texture) // checks if texture exists
                    texture.needsUpdate = true;
            };

            var material = new THREE.MeshBasicMaterial({ map: texture, side: THREE.DoubleSide });
            material.transparent = true;

            var mesh = new THREE.Mesh(
                new THREE.PlaneGeometry(canvas.width, canvas.height),
                material
            );
            //scene.add( mesh2 );
            return mesh
        }


        //Introducing Real Loop ---------------------------------------------
        let positionCordinateData = [
            {
                "xAxis": 0,
                "yAxis": 250,
                "zAxis": -2000
            },
            {
                "xAxis": 1000,
                "yAxis": 250,
                "zAxis": -2000
            },
            {
                "xAxis": 2000,
                "yAxis": 250,
                "zAxis": -2000
            },
            {
                "xAxis": 3000,
                "yAxis": 250,
                "zAxis": -2000
            },
            {
                "xAxis": 4000,
                "yAxis": 250,
                "zAxis": -2000
            },
            {
                "xAxis": -1000,
                "yAxis": 250,
                "zAxis": -2000
            },
            {
                "xAxis": -2000,
                "yAxis": 250,
                "zAxis": -2000
            },
            {
                "xAxis": -3000,
                "yAxis": 250,
                "zAxis": -2000
            },
        ];







        /*for (let i = 0; i < 1; ++i) {

            var source = data[i]["Data"]
            puzzel_1 = uploadImage(source);
            puzzel_1.position.set(1000, 250, -2000);
            scene.add(puzzel_1);

        }*/
        let currentImage;
        for (let i = 0; i < photoData.length; ++i) {
            currentImage = uploadImage(photoData[i].srcData);
            currentImage.position.set(positionCordinateData[i].xAxis, positionCordinateData[i].yAxis, positionCordinateData[i].zAxis);
            scene.add(currentImage);
        }


        //puzzel_2 = uploadImage(photoData[0].srcData);
        //puzzel_2.position.set(0, 250, -2000);
        //scene.add(puzzel_2);




        // puzzel vectors ---------------------
        /*puzzel_1 = uploadImage('images/puzzel_pics/image_123923953(1).JPG');
        puzzel_1.position.set(0, 250, -2000);
        scene.add(puzzel_1);*/

        // puzzel vectors ---------------------
        /*puzzel_2 = uploadImage('images/puzzel_pics/image_123923953(2).JPG');
        puzzel_2.position.set(1000, 250, -2000);
        scene.add(puzzel_2);*/

        /*// puzzel vectors ---------------------
        puzzel_3 = uploadImage('images/puzzel_pics/image_123923953(3).JPG');
        puzzel_3.position.set(2000, 250, -2000);
        scene.add(puzzel_3);

        // puzzel vectors ---------------------
        puzzel_4 = uploadImage('images/puzzel_pics/image_123923953(4).JPG');
        puzzel_4.position.set(3000, 250, -2000);
        scene.add(puzzel_4);

        // puzzel vectors ---------------------
        puzzel_5 = uploadImage('images/puzzel_pics/image_123923953(5).JPG');
        puzzel_5.position.set(4000, 250, -2000);
        scene.add(puzzel_5);

        // puzzel vectors ---------------------
        puzzel_6 = uploadImage('images/puzzel_pics/image_123923953(6).JPG');
        puzzel_6.position.set(-1000, 250, -2000);
        scene.add(puzzel_6);

        // puzzel vectors ---------------------
        puzzel_7 = uploadImage('images/puzzel_pics/image_123923953(7).JPG');
        puzzel_7.position.set(-2000, 250, -2000);
        scene.add(puzzel_7);

        // puzzel vectors ---------------------
        puzzel_8 = uploadImage('images/puzzel_pics/image_123923953(8).JPG');
        puzzel_8.position.set(-3000, 250, -2000);
        scene.add(puzzel_8);

        // puzzel vectors ---------------------
        puzzel_9 = uploadImage('images/puzzel_pics/image_123923953(9).JPG');
        puzzel_9.position.set(-4000, 250, -2000);
        scene.add(puzzel_9);

        // puzzel vectors ---------------------
        puzzel_10 = uploadImage('images/puzzel_pics/image_123923953(10).JPG');
        puzzel_10.position.set(1000, 250, -4000);
        scene.add(puzzel_10);

        // puzzel vectors ---------------------
        puzzel_11 = uploadImage('images/puzzel_pics/image_123923953(11).JPG');
        puzzel_11.position.set(-1000, 250, -4000);
        scene.add(puzzel_11);

        // puzzel vectors ---------------------
        puzzel_12 = uploadImage('images/puzzel_pics/image_123923953(12).JPG');
        puzzel_12.position.set(2000, 250, -4000);
        scene.add(puzzel_12);

        // puzzel vectors ---------------------
        puzzel_13 = uploadImage('images/puzzel_pics/image_123923953(13).JPG');
        puzzel_13.position.set(-2000, 250, -4000);
        scene.add(puzzel_13);

        // puzzel vectors ---------------------
        puzzel_14 = uploadImage('images/puzzel_pics/image_123923953(14).JPG');
        puzzel_14.position.set(3000, 250, -4000);
        scene.add(puzzel_14);

        // puzzel vectors ---------------------
        puzzel_15 = uploadImage('images/puzzel_pics/image_123923953(15).JPG');
        puzzel_15.position.set(-3000, 250, -4000);
        scene.add(puzzel_15);

        // puzzel vectors ---------------------
        puzzel_16 = uploadImage('images/puzzel_pics/image_123923953(16).JPG');
        puzzel_16.position.set(4000, 250, -4000);
        scene.add(puzzel_16);

        // puzzel vectors ---------------------
        puzzel_17 = uploadImage('images/puzzel_pics/image_123923953(17).JPG');
        puzzel_17.position.set(-4000, 250, -4000);
        scene.add(puzzel_17);

        // puzzel vectors ---------------------
        puzzel_18 = uploadImage('images/puzzel_pics/image_123923953(18).JPG');
        puzzel_18.position.set(0, 250, 1500);
        puzzel_18.rotation.y = Math.PI;
        scene.add(puzzel_18);

        // puzzel vectors ---------------------
        puzzel_19 = uploadImage('images/puzzel_pics/image_123923953(19).JPG');
        puzzel_19.position.set(1000, 250, 1500);
        puzzel_19.rotation.y = Math.PI;
        scene.add(puzzel_19);

        // puzzel vectors ---------------------
        puzzel_20 = uploadImage('images/puzzel_pics/image_123923953(20).JPG');
        puzzel_20.position.set(-1000, 250, 1500);
        puzzel_20.rotation.y = Math.PI;
        scene.add(puzzel_20);

        // puzzel vectors ---------------------
        puzzel_21 = uploadImage('images/puzzel_pics/image_123923953(21).JPG');
        puzzel_21.position.set(2000, 250, 1500);
        puzzel_21.rotation.y = Math.PI;
        scene.add(puzzel_21);

        // puzzel vectors ---------------------
        puzzel_22 = uploadImage('images/puzzel_pics/image_123923953(22).JPG');
        puzzel_22.position.set(-2000, 250, 1500);
        puzzel_22.rotation.y = Math.PI;
        scene.add(puzzel_22);

        // puzzel vectors ---------------------
        puzzel_23 = uploadImage('images/puzzel_pics/image_123923953(23).JPG');
        puzzel_23.position.set(3000, 250, 1500);
        puzzel_23.rotation.y = Math.PI;
        scene.add(puzzel_23);

        // puzzel vectors ---------------------
        puzzel_24 = uploadImage('images/puzzel_pics/image_123923953(24).JPG');
        puzzel_24.position.set(-3000, 250, 1500);
        puzzel_24.rotation.y = Math.PI;
        scene.add(puzzel_24);

        // puzzel vectors ---------------------
        puzzel_25 = uploadImage('images/puzzel_pics/image_123923953(25).JPG');
        puzzel_25.position.set(4000, 250, 1500);
        puzzel_25.rotation.y = Math.PI;
        scene.add(puzzel_25);

        // puzzel vectors ---------------------
        puzzel_26 = uploadImage('images/puzzel_pics/image_123923953(26).JPG');
        puzzel_26.position.set(-4000, 250, 1500);
        puzzel_26.rotation.y = Math.PI;
        scene.add(puzzel_26);

        // puzzel vectors ---------------------
        puzzel_27 = uploadImage('images/puzzel_pics/image_123923953(27).JPG');
        puzzel_27.position.set(-1000, 250, 3500);
        puzzel_27.rotation.y = Math.PI;
        scene.add(puzzel_27);

        // puzzel vectors ---------------------
        puzzel_28 = uploadImage('images/puzzel_pics/image_123923953(28).JPG');
        puzzel_28.position.set(1000, 250, 3500);
        puzzel_28.rotation.y = Math.PI;
        scene.add(puzzel_28);

        // puzzel vectors ---------------------
        puzzel_29 = uploadImage('images/puzzel_pics/image_123923953.JPG');
        puzzel_29.position.set(2000, 250, 3500);
        puzzel_29.rotation.y = Math.PI;
        scene.add(puzzel_29);*/
        //

        container = document.getElementById('puzzel_environment');

        renderer = new THREE.WebGLRenderer({ antialias: true });
        renderer.setPixelRatio(window.devicePixelRatio);
        //renderer.setSize(window.innerWidth, window.innerHeight);
        renderer.setSize(document.getElementById('puzzel_environment').clientWidth, parent.innerHeight);

        container.appendChild(renderer.domElement);

        //

        window.addEventListener('resize', onWindowResize);

    }

    function onWindowResize() {

        camera.aspect = window.innerWidth / window.innerHeight;
        camera.updateProjectionMatrix();

        //renderer.setSize(window.innerWidth, window.innerHeight);
        renderer.setSize(document.getElementById('puzzel_environment').clientWidth, parent.innerHeight);


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
    //}

    //export function Make

    //export function {init, animate}


});