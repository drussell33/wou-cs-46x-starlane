import * as THREE from './three.module.js';
import Stats from './stats.module.js';
import { OrbitControls } from './OrbitControls.js';
import { Water } from './Water.js';
import { Sky } from './Sky.js';
import { GatherPhotoData, LoadImagesToScene, LoadDemoImagesToScene } from './environment_functions.js';


$(document).ready(function MakeGallery() {
    console.log("page loaded the ocean environment.js");




    //Gets the photo data written to the tr's in the DOM
    var photoData = [];
    photoData = GatherPhotoData(photoData);


    let photoDemoData = [
        { "srcData": './images/fish_pics/fish4.png', "srcTitle": 'Trout', "srcRank": 1, "srcDescription": "Caught Aug 2017" },
        { "srcData": './images/fish_pics/fish2.png', "srcTitle": 'Big Mouth Bass', "srcRank": 1, "srcDescription": "Caught Jun 2013" },
        { "srcData": './images/fish_pics/fish3.png', "srcTitle": 'Big Mouth Bass', "srcRank": 1, "srcDescription": "Caught Jun 2013" },
        { "srcData": './images/fish_pics/marlin.png', "srcTitle": 'Big Mouth Bass', "srcRank": 1, "srcDescription": "Caught Jun 2013" },
        { "srcData": './images/fish_pics/bluegill.png', "srcTitle": 'Big Mouth Bass', "srcRank": 1, "srcDescription": "Caught Jun 2013" },
        { "srcData": './images/fish_pics/steelhead.png', "srcTitle": 'Big Mouth Bass', "srcRank": 1, "srcDescription": "Caught Jun 2013" },
        { "srcData": './images/fish_pics/boot.png', "srcTitle": 'Big Mouth Bass', "srcRank": 1, "srcDescription": "Caught Jun 2013" },
        { "srcData": './images/fish_pics/rockcod.png', "srcTitle": 'Big Mouth Bass', "srcRank": 1, "srcDescription": "Caught Jun 2013" },
    ];




    if (photoData.length === 0) {
        console.log("Photo Data is NUll")
    }




    var container, stats;
    var camera, scene, renderer;
    var controls, water, sun, mesh, fish4, fish3, fish2, bluegill, boot, marlin, rockcod, salmon, steelhead, trout;
    var fish4Text,
        fish3Text,
        fish2Text,
        bluegillText,
        bootText,
        marlinText,
        rockcodText,
        salmonText,
        steelheadText,
        troutText;

    init();
    animate();

    function init() {

        container = document.getElementById('ocean_container2');

        //

        renderer = new THREE.WebGLRenderer();
        renderer.setPixelRatio(window.devicePixelRatio);
        renderer.setSize(document.getElementById('ocean_container2').clientWidth, parent.innerHeight);
        //renderer.setSize(window.innerWidth, window.innerHeight);
        container.appendChild(renderer.domElement);

        //

        scene = new THREE.Scene();

        camera = new THREE.PerspectiveCamera(55, window.innerWidth / window.innerHeight, 1, 20000);
        camera.position.set(100, 30, 100);

        //

        sun = new THREE.Vector3();

        // Water

        var waterGeometry = new THREE.PlaneGeometry(10000, 10000);

        water = new Water(
            waterGeometry,
            {
                textureWidth: 512,
                textureHeight: 512,
                waterNormals: new THREE.TextureLoader().load('./images/textures/waternormals.jpg',
                    function (texture) {

                        texture.wrapS = texture.wrapT = THREE.RepeatWrapping;

                    }),
                alpha: 1.0,
                sunDirection: new THREE.Vector3(),
                sunColor: 0xffffff,
                waterColor: 0x001e0f,
                distortionScale: 3.7,
                fog: scene.fog !== undefined
            }
        );

        water.rotation.x = - Math.PI / 2;

        scene.add(water);

        // Skybox

        var sky = new Sky();
        sky.scale.setScalar(10000);
        scene.add(sky);

        var skyUniforms = sky.material.uniforms;

        skyUniforms['turbidity'].value = 10;
        skyUniforms['rayleigh'].value = 2;
        skyUniforms['mieCoefficient'].value = 0.005;
        skyUniforms['mieDirectionalG'].value = 0.8;

        var parameters = {
            inclination: 0.49,
            azimuth: 0.205
        };

        var pmremGenerator = new THREE.PMREMGenerator(renderer);

        function updateSun() {

            var theta = Math.PI * (parameters.inclination - 0.5);
            var phi = 2 * Math.PI * (parameters.azimuth - 0.5);

            sun.x = Math.cos(phi);
            sun.y = Math.sin(phi) * Math.sin(theta);
            sun.z = Math.sin(phi) * Math.cos(theta);

            sky.material.uniforms['sunPosition'].value.copy(sun);
            water.material.uniforms['sunDirection'].value.copy(sun).normalize();

            scene.environment = pmremGenerator.fromScene(sky).texture;

        }

        updateSun();


        //Custom Upload Photo
        function uploadDemoImage(collectionPhoto) {
            // create a canvas element
            var canvas = document.createElement('canvas');
            canvas.width = 500;
            canvas.height = 500;
            var context = canvas.getContext('2d');
            // canvas contents will be used for a texture
            var texture = new THREE.Texture(canvas);

            // load an image
            var imageObj = new Image();
            imageObj.src = collectionPhoto;
            // after the image is loaded, this function executes
            imageObj.onload = function () {
                context.drawImage(imageObj, 0, 0, imageObj.width, imageObj.height, 0, 0, 150, 100);
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


        /////// draw text on canvas /////////

        // create a canvas element
        function inputText(name, caught, location) {


            var canvas1 = document.createElement('canvas');
            canvas1.width = 150;
            canvas1.height = 75;
            var context1 = canvas1.getContext('2d');
            context1.font = "Bold 12px Arial";
            context1.fillStyle = "rgba(0,0,0)";
            context1.fillText(name, 20, 30);
            context1.fillText(caught, 20, 50);
            context1.fillText(location, 20, 70);

            // canvas contents will be used for a texture
            var texture1 = new THREE.Texture(canvas1);
            texture1.needsUpdate = true;

            var material1 = new THREE.MeshBasicMaterial({ map: texture1, side: THREE.DoubleSide });
            material1.transparent = true;

            var mesh1 = new THREE.Mesh(
                new THREE.PlaneGeometry(canvas1.width, canvas1.height),
                material1
            );

            return mesh1;
        }




        let positionCordinateData = [
            //Row One
            { "xAxis": 0, "yAxis": 75, "zAxis": -700, "yRotation": null },
            { "xAxis": 700, "yAxis": 75, "zAxis": 0, "yRotation": -1.57 },
            { "xAxis": 0, "yAxis": 75, "zAxis": 700, "yRotation": Math.PI },
            { "xAxis": -700, "yAxis": 75, "zAxis": 0, "yRotation": 1.57 },
            { "xAxis": 700, "yAxis": 75, "zAxis": -700, "yRotation": -0.78},
            { "xAxis": -700, "yAxis": 75, "zAxis": -700, "yRotation": 0.78 },
            { "xAxis": 700, "yAxis": 75, "zAxis": 700, "yRotation": 3.92 },
            { "xAxis": -700, "yAxis": 75, "zAxis": 700, "yRotation": -3.92 },
        ];


        LoadImagesToScene(scene, photoData, positionCordinateData);

        if (photoData.length === 0) {
            LoadDemoImagesToScene(scene, photoDemoData, positionCordinateData);
        }


       /* // CUstom text to sign THIRD Attempt END ----------------------------------------------------------------------------

        // adding fish ---------------------
        fish4 = uploadImage('./images/fish_pics/fish4.png');
        fish4.position.set(0, 25, -300);
        //fish4.rotation.y = Math.PI / 2;
        //fish4Text = uploadText("WILL THIS WORK!!!")
        fish4Text = inputText('Trout', "Caught Aug 2017", "Hag Lake Oregon");
        //fish4Text = createLabel("HELLO WORLD", 24, "black")
        fish4Text.position.set(0, 130, -300);
        scene.add(fish4);
        scene.add(fish4Text);


        // adding fish ---------------------
        fish2 = uploadImage('./images/fish_pics/fish2.png');
        fish2.position.set(300, 25, 0);
        fish2.rotation.y = Math.PI / 2;
        fish2Text = inputText('Big Mouth Bass', "Caught Jun 2013", "Vernoia Lake");
        fish2Text.position.set(300, 130, 0);
        fish2Text.rotation.y = 4.6;
        scene.add(fish2);
        scene.add(fish2Text);

        // adding fish ---------------------
        fish3 = uploadImage('./images/fish_pics/fish3.png');
        fish3.position.set(0, 25, 300);
        fish3Text = inputText('Big Mouth Bass', "Caught Jun 2013", "Vernoia Lake");
        fish3Text.position.set(0, 130, 300);
        fish3Text.rotation.y = 3;
        scene.add(fish3);
        scene.add(fish3Text);

        // adding fish ---------------------
        marlin = uploadImage('./images/fish_pics/marlin.png');
        marlin.position.set(-300, 25, 0);
        marlin.rotation.y = Math.PI / 2;
        scene.add(marlin);

        marlinText = inputText('Marlin', "Caught Dec 2016", "Gulf of Mexico");
        marlinText.position.set(-300, 130, 0);
        marlinText.rotation.y = 1.3;
        scene.add(marlinText);

        // adding fish ---------------------
        bluegill = uploadImage('./images/fish_pics/bluegill.png');
        bluegill.position.set(-300, 25, 300);
        bluegill.rotation.y = -60;
        scene.add(bluegill);

        bluegillText = inputText('Bluegill', "Caught May 2009", "Crater Lake");
        bluegillText.position.set(-300, 130, 300);
        bluegillText.rotation.y = 2.3;
        scene.add(bluegillText);

        // adding fish ---------------------
        steelhead = uploadImage('./images/fish_pics/steelhead.png');
        steelhead.position.set(-200, 25, -200);
        steelhead.rotation.y = 60;
        scene.add(steelhead);

        steelheadText = inputText('SteelHead', "Caught Sep 2010", "Columbia River");
        steelheadText.position.set(-200, 130, -200);
        steelheadText.rotation.y = .3;
        scene.add(steelheadText);

        // adding fish ---------------------
        boot = uploadImage('./images/fish_pics/boot.png');
        boot.position.set(200, 25, 200);
        boot.rotation.y = 60;
        scene.add(boot);

        bootText = inputText('Boot', "Caught Feb 2021", "Atlantic Ocean");
        bootText.position.set(200, 130, 200);
        bootText.rotation.y = 60;
        scene.add(bootText);

        // adding fish ---------------------
        rockcod = uploadImage('./images/fish_pics/rockcod.png');
        rockcod.position.set(250, 25, -250);
        rockcod.rotation.y = 50;
        scene.add(rockcod);

        rockcodText = inputText('Rock Cod', "Caught Oct 2017", "Newberg Oregon Coast");
        rockcodText.position.set(250, 130, -250);
        rockcodText.rotation.y = 50;
        scene.add(rockcodText);
        */
        // ----------------------------------------------
        controls = new OrbitControls(camera, renderer.domElement);
        controls.maxPolarAngle = Math.PI * 0.495;
        controls.target.set(0, 10, 0);
        controls.minDistance = 40.0;
        controls.maxDistance = 200.0;
        controls.update();

        //

        stats = new Stats();
        //container.appendChild(stats.dom);
        //container.append(stats.dom)


        window.addEventListener('resize', onWindowResize);

    }

    function onWindowResize() {

        camera.aspect = window.innerWidth / window.innerHeight;
        camera.updateProjectionMatrix();

        renderer.setSize(window.innerWidth, window.innerHeight);

    }

    function animate() {

        requestAnimationFrame(animate);
        render();
        stats.update();

    }

    function render() {

        var time = performance.now() * 0.001;

        //mesh.position.y = Math.sin( time ) * 20 + 5;
        //fish2.position.y = Math.sin(time) * 20 + 5;
        //fish3.position.y = Math.sin(time - 1) * 20 + 5;
        //fish4.position.y = Math.sin(time - 2) * 20 + 5;

        //bluegill.position.y = Math.sin(time) * 20 + 5;
        //boot.position.y = Math.sin(time - 3) * 20 + 5;
        //marlin.position.y = Math.sin(time - 4) * 20 + 5;

        //rockcod.position.y = Math.sin(time - 5) * 20 + 5;
        //steelhead.position.y = Math.sin(time - 6) * 20 + 5;
        //mesh.rotation.x = time * 0.5;
        //mesh.rotation.z = time * 0.51;

        water.material.uniforms['time'].value += 1.0 / 60.0;

        renderer.render(scene, camera);

    }

});