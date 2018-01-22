﻿open System


let compose = fun f -> fun g -> fun x -> g(f(x))
let first = fst
let c2 = (>>)

[<EntryPoint>]
let main argv =
    List.iter (Ast.tryExp Ast.myEnv) Ast.examples

    [Ast2.example1; Ast2.example2; Ast2.example3; Ast2.example4; Ast2.example5]
    |> List.iter (fun e -> Ast2.resetId()
                           let ty = Ast2.infer Ast2.Env.empty 0 e
                           match ty with
                               | Ast2.TConst  name -> printfn "tconst"
                               | Ast2.TApp(ty,tylist) -> printfn "tapp"
                               | Ast2.TArrow(tylist, ty) -> printfn "tarrow: params: %A, ty: %A" tylist ty
                               | Ast2.TVar(tvarref) -> printfn "tvar"

                           let generalizedTy = Ast2.generalize (-1) ty
                           match generalizedTy with
                               | Ast2.TConst  name -> printfn "tconst"
                               | Ast2.TApp(ty,tylist) -> printfn "tapp"
                               | Ast2.TArrow(tylist, ty) -> printfn "tarrow: params: %A, ty: %A" tylist ty
                               | Ast2.TVar(tvarref) -> printfn "tvar"
                           printfn "%s" (Ast2.stringOfExpr e)
                           printfn ": %s" (Ast2.stringOfTy generalizedTy) )
    0

