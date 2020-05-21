export class UserRole {
   name: string;
   value: number;
   constructor(name: string, id: number){
       this.name = name;
       this.value = id;
   }
}

export const Administrator: UserRole = new UserRole('Administrator', 2);
export const Student: UserRole = new UserRole('Student', 0);
export const Supervisor: UserRole = new UserRole('Supervisor', 1);
