FeatureScript 1589;
import(path : "onshape/std/geometry.fs", version : "1589.0");

import(path : "2fa81f50be25609bc956cd5f/9315fcf8489f0c0cc1a06a01/40a6bde79e4081741060af59", version : "24d9ce4bf05b3add5d64a574");
import(path : "ff0f26334dc9ed5e1dbbc027", version : "85850ebabbc641188e9efe11");
baffleModule::import(path : "c2f4584cf9d8b1114f7ff5b4", version : "52d3f8c284e140156578bdad");
import(path : "c81fce53dede81ef89860aa3/143d08de7750ac406af6ad04/b453944163f91ccf1477e3f0", version : "d3e1c56c5ddb796b94cd62ce");





export const baffleSetTree =
{
        name : "baffleSet",
        notes : {
            description : "",
            imagelink : "",
            textbooklink : "",
        },
        designers : {
            pre : baffleSetPreDesigner,
            //post : baffleSetPostDesigner,
            geometry : baffleSetGeometry,
        },
        params : {
            rep : true,
            ip : "app",
            channelN : [0, 4, 100], //number of baffle sets
            channelT : [0, 0.2, 1], //thickness of channel
            channelW : [0.05, 0.5, 100],
            channelL : [0, 7, 200],
            tankH : [1, 2, 200],
            FB : [0, 0.1, 1],
            baffleT : [0, 0.0008, 2],
            baffleS : [0.01, 0.1, 10],
            HL_bod : [0, 0.4, 1],
            washerT : [0.001, 0.003175, 0.2],

        },
        children : {
            "baffle" : {
                tree : baffleTree,
                inputs : {

                    rep : "$.rep",
                    ip : "$.ip",
                    lastchannel : "false", //diff
                    tankH : "$.tankH",
                    channelW : "$.channelW",
                    channelL : "$.channelL",
                    FB : "$.FB",
                    baffleT : "$.baffleT",
                    baffleS : "$.baffleS",
                    HL_bod : "$.HL_bod",
                    washerT : "$.washerT",

                },
            },
        },
    };

export const baffleSetPreDesigner = function(design) returns map
    {

    design.channelB = design.channelW + design.channelT;

        return design;

    };

export const baffleSetPostDesigner = function(design) returns map
    {
        return design;

    };


export const baffleSetGeometry = function(context is Context, id is Id, design is map) returns map
    {
        for (var i = 1; i <= design.channelN; i += 1) //for every discrete number from 1 to N
        {


            if (i == design.channelN) //last channel?
            {
                design.lastchannel = true;
            }
            else
            {
                design.lastchannel = false;
            }

            if  (i%2==0) //if even, rotation of baffle
            {
                design.originY = (-design.channelL) / meter; //location of origin, ADJUST
                design.originV1 = vector(-1, 0, 0) * meter; //new rotation
                design.originV2 = vector(0, 0, 1) * meter;
                design.originX = (-design.channelB * (i - 1) - design.channelW) / meter;
            }

            else
            {
                design.originY = 0; //location of origin
                design.originV1 = vector(1, 0, 0) * meter; //original rotation
                design.originV2 = vector(0, 0, 1) * meter;
                design.originX = -design.channelB * (i - 1) / meter; //horizontal placement
            }


            var qlocation = coordSystem(vector(design.originX, design.originY, 0) * meter, design.originV1, design.originV2); //these are vectors, they do intersect at a certain point (rn it is the origin)

            opMateConnector(context, id + i, { 'coordSystem' : qlocation });
            const mateQ = qCreatedBy(id + i, EntityType.VERTEX);
            print(mateQ);
            const args = { mcName : "", includeVariables : false, variablePrefix : "", variableSuffix : "", suppressGeometry : false, operationType : NewBodyOperationType.NEW, customConfiguration : false, configEntries : configEntriesDefault, configurationString : configurationStringDefault, useOverrides : false };
            const customArgs = {
                        "partStudio" : { buildFunction : baffleModule::build, configuration : {"overrides": mapToJSON(serializer(design.baffle)) } } as PartStudioData,
                        location : mateQ
                    };
            superDerive(context, id + i, mergeMaps(args, customArgs));

        }
        return design;

    };

annotation { "Feature Type Name" : "Baffle Set" }
export const baffleSetFeature = defineFeature(function(context is Context, id is Id, definition is map)
    precondition
    {
    }
    {
        treeInstantiatorFeature(context, id, baffleSetTree as InputTree);
    });

//to do:
// - for last channel, make sure that the last spacer accounts for the different number of baffles
