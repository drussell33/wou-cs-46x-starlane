//import {$} from '../../iCollections/wwwroot/lib/jquery/dist/jquery.js';
import * as $ from '../../iCollections/wwwroot/lib/jquery/dist/jquery.min.js';

$ = require('jquery'); //load the jQuery module (installed from npm)
window.$ = $;

global.Maybe = require('@testing-library/jest-dom');
//import '@testing-library/jest-dom';
//import { screen } from '@testing-library/jest-dom';

//global.ascreen = screen;
//global.window = window;
global.$ = global.jQuery = $;
//global['$'] = global['jQuery'] = $;
global.Derek = 'derek';


