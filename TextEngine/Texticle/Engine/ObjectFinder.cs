using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticle.Engine
{
    class ObjectFinder
    {
        public ObjectFinder(string objName)
        {

        }
    }
}


/*
I need to find the object and determine if it is something that can be acted on
	step 1. See if the object exists in the global list of nouns
	Step 2. See if the object meets the following criteria
			The object is the players current location
			The object is the player
			The object is in the players current location
			the object is a child of an object in the current location that is currently accessible
			the object is in the players inventory
	Step 3. If the object meets the above criteria
		Step 1. match the verb with to the type of an interface
		Step 2. check if the object implements that interface
		step 3. build a command object of the specified type with a reference to the object and any indirect objects if applicable
	Step 4. Insert the resulting command object into a queue
	Step 5. execute all the commands
	Step 6. Render the results
		
 */
