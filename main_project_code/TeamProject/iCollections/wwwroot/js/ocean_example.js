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
    var controls, water, sun;
 

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


       /* Original hardcoded examples ----------------------------------------------------------------------------

        // adding fish ---------------------
        fish4 = uploadImage('./images/fish_pics/fish4.png');
        fish4.position.set(0, 25, -300);
        //fish4.rotation.y = Math.PI / 2;
        //fish4Text = uploadText("WILL THIS WORK!!!")
        fish4Text = inputText('Trout', "Caught Aug 2017", "Hag Lake Oregon");
        //fish4Text = createLabel("HELLO WORLD", 24, "black")
        fish4Text.position.set(0, 130, -300);
        scene.add(fish4);
        scene.add(fish4Text); */

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

        //original movement
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