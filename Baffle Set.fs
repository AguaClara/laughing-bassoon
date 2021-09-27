FeatureScript 1589;
import(path : "onshape/std/geometry.fs", version : "1589.0");

import(path : "c2f4584cf9d8b1114f7ff5b4", version : "dfbdf5c0b96207877ddc5578");


export const bafflesetTree =
{
        name : "baffleset",
        notes : {
            description : "",
            imagelink : "",
            textbooklink : "",
        },
        designers : {
            pre : bafflesetPreDesigner,
            post : bafflesetPostDesigner,
        },
        params : {
            rep : true,
            ip : "app",
            N: [0, 5, 100]; //number of baffle sets
            
            


        },
        children : {
            "baffle" : {
                tree : baffleTree,
                inputs : {
                    // T : "$.baffleT", //thickness
                    // L : "$.bafflebottomL", //length
                    // W : "$.channelW", //width, sheet width same as channel width
                    // t : "corrugated", //type
                    // mat : "PC", //material
                    // ip : "$.ip",
            
                    
                    // rep : "$.rep",
                    // ip : "$.ip",
                    // flowfront : "false",
                    // lastchannel : "false",
                    // tankH : "$.tankH",
                    // channelW : ,
                    // channelL : ,
                    // FB : ,
                    // baffleT :,
                    // baffleS : ,
                    // HL_bod : ,
                    // washerT : ,
                    // washerOD : ,

                },
            },
        },
    };

export const bafflesetPreDesigner = function(design) returns map
    {

        return design;

    };

export const bafflesetPostDesigner = function(design) returns map
    {

        return design;

    };


annotation { "Feature Type Name" : "Baffle Set" }
export const bafflesetFeature = defineFeature(function(context is Context, id is Id, definition is map)
    precondition
    {
    }
    {
        treeInstantiatorFeature(context, id, bafflesetTree as InputTree);
    });
    
    
    
    //to do list
    // - define params
    // - define inputs
