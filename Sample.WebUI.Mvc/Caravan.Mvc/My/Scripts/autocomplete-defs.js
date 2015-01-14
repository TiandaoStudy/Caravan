/*******************************************************************************
 * Employee
 *******************************************************************************/

function getEmployeesByNameOrID(term) {
   return "$filter=substringof('" + term + "',FirstName) eq true or substringof('" + term + "',LastName) eq true";
}

function mapEmployeesForSelect2(employees) {
   return _.map(employees, function(e) {
      return { id: e.EmployeeID, text: e.FirstName + " " + e.LastName, disabled: false };
   });
}