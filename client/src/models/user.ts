// class User {
//   constructor(id, name, email) {
//     this.id = id;
//     this.name = name;
//     this.email = email;
//   }
// }

//TODO: Brukes denne?
interface UserT {
  id: string;
  name: string;
  email: string;
}
export const User = ({ id, name, email }: UserT) => {
  return { id, name, email };
};
