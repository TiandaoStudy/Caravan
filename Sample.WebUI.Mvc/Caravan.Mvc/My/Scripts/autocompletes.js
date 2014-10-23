function getEmployeesByNameOrID(term) {
   return "$filter=substringof('" + term + "',FirstName) eq true or substringof('" + term + "',LastName) eq true";
}

function mapEmployeeForSelect2(employee) {
   return { id: employee.EmployeeID, text: employee.FirstName + " " + employee.LastName, disabled: false };
}